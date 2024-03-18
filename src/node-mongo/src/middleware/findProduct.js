import Product from '../models/productModel.js';

// Middleware to find a product by its ID
const findProduct = async (req, res, next) => {
  try {
    const product = await Product.findById(req.params.id);
    if (!product) {
      res.status(404).json({ message: 'Product not found' });
      return;
    }
    req.product = product; // Add the product to the request object
    next(); // Pass control to the next middleware function
  } catch (error) {
    next(error); // Pass any errors to the next middleware function
  }
};

export default findProduct;
