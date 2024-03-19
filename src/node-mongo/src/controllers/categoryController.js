import Category from '../models/categoryModel.js';

export const getCategories = async (req, res) => {
  const categories = await Category.find();
  res.status(200).json(categories);
};

export const getCategoryById = async (req, res) => {
  const category = await Category.findById(req.params.id);
  if (!category) {
    res.status(404).json({ message: 'Category not found' });
    return;
  }
  res.status(200).json(category);
};

export const createCategory = async (req, res) => {
  const category = new Category(req.body);
  try {
    await category.save();
    res.status(201).json({ message: 'Category created', category });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

export const updateCategory = async (req, res) => {
  // Check if the category exists
  const { id } = req.params;
  const category = await Category.findById(id);
  if (!category) {
    res.status(404).json({ message: 'Category not found' });
    return;
  }

  await Category.findByIdAndUpdate(req.params.id, req.body, { new: true });
  res.status(200).json({ message: 'Category updated', category });
};

export const patchCategory = async (req, res) => {
  // Check if the category exists
  const { id } = req.params;
  const category = await Category.findById(id);
  if (!category) {
    res.status(404).json({ message: 'Category not found' });
    return;
  }

  try {
    const category = await Category.findById(id);
    if (!category) {
      return res.status(404).json({ message: 'Category not found' });
    } else {
      Object.keys(req.body).forEach((key) => (category[key] = req.body[key]));
      await category.save();
      res.status(200).json({ message: 'Category updated', category });
    }
  } catch (error) {
    res.status(400).json({ message: error.message });
  }
};

export const deleteCategory = async (req, res) => {
  // Check if the category exists
  const { id } = req.params;
  const category = await Category.findById(id);
  if (!category) {
    res.status(404).json({ message: 'Category not found' });
    return;
  }
  await Category.findByIdAndDelete(req.params.id);
  res.status(200).json({ message: 'Category deleted' });
};
