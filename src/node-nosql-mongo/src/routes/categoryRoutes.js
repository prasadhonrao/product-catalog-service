import express from 'express';

import {
  getCategories,
  getCategoryById,
  createCategory,
  updateCategory,
  patchCategory,
  deleteCategory,
} from '../controllers/categoryController.js';

const categoryRoutes = express.Router();

categoryRoutes.get('/', getCategories);
categoryRoutes.get('/:id', getCategoryById);
categoryRoutes.post('/', createCategory);
categoryRoutes.put('/:id', updateCategory);
categoryRoutes.patch('/:id', patchCategory);
categoryRoutes.delete('/:id', deleteCategory);

export default categoryRoutes;
