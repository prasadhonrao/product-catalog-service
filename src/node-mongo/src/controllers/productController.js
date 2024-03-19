import Product from '../models/productModel.js';

// All your controller functions go here
export const getProducts = async (req, res) => {
  let query = Product.find();
  const { category, name, order } = req.query;

  // Filter products by category
  if (category) {
    query = query.where('category', new RegExp(category, 'i'));
  }

  // Search products by name
  if (name) {
    query = query.where('productName', new RegExp(name, 'i'));
  }

  // Sort products by price
  if (order) {
    query = query.sort({ price: order === 'desc' ? -1 : 1 });
  }

  try {
    const products = await query.exec();
    res.status(200).json(products);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

export const getProductById = async (req, res) => {
  try {
    const product = req.product;
    res.status(200).json(product);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

export const createProduct = async (req, res) => {
  const product = new Product(req.body);

  // Validate the product
  const validationError = product.validateSync();
  if (validationError) {
    return res.status(400).json({ message: validationError.message });
  }

  try {
    await product.save();
    res.status(201).json({ message: 'Product created', product });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

export const updateProduct = async (req, res) => {
  // Check if _id is being updated
  if (req.body._id) {
    return res.status(400).json({ message: 'Cannot update _id' });
  }

  const product = req.product;

  // Create a new product with the request body
  const incomingProduct = new Product(product);

  // Validate the updated product
  const validationError = incomingProduct.validateSync();
  if (validationError) {
    return res.status(400).json({ message: validationError.message });
  }

  await Product.findByIdAndUpdate(req.params.id, req.body, { new: true });
  res.status(200).json({ message: 'Product updated', incomingProduct });
};

export const patchProduct = async (req, res) => {
  // Check if _id is being updated
  if (req.body._id) {
    return res.status(400).json({ message: 'Cannot update _id' });
  }

  const product = req.product;
  const validUpdates = Object.keys(Product.schema.paths).filter((path) => path !== '_id'); // Exclude _id from valid fields

  // Check if all updates are valid
  const updates = Object.keys(req.body);
  const isValidUpdate = updates.every((update) => validUpdates.includes(update));

  if (!isValidUpdate) {
    return res.status(400).json({ message: 'Invalid field name in update' });
  }

  // Update the fields of the product with the request body
  updates.forEach((update) => (product[update] = req.body[update]));

  try {
    await product.save();

    res.status(200).json({ message: 'Product updated', product });
  } catch (error) {
    res.status(400).json({ message: error.message });
  }
};

export const deleteProduct = async (req, res) => {
  const { id } = req.params;
  await Product.findByIdAndDelete(id);
  res.status(200).json({ message: 'Product deleted' });
};
