import Category from '../models/categoryModel.js';

// Middleware to validate if the categories sent in the request exist in the database
const validateCategories = async (req, res, next) => {
  if (req.body.categories) {
    const categories = req.body.categories;
    const categoryExists = await Category.find({ _id: { $in: categories } });
    if (categories.length !== categoryExists.length) {
      return res.status(400).json({ message: 'One or more categories do not exist' });
    }
  }
  next();
};

export default validateCategories;
