import express from 'express';
import validateProductId from '../middleware/validateProductId.js';
import findProduct from '../middleware/findProduct.js';
import validateCategories from '../middleware/validateCategories.js';
import {
  getProducts,
  getProductById,
  createProduct,
  updateProduct,
  patchProduct,
  deleteProduct,
} from '../controllers/productController.js';

const productRoutes = express.Router();

productRoutes.get('/', getProducts);
productRoutes.get('/:id', validateProductId, findProduct, getProductById);
productRoutes.post('/', validateCategories, createProduct);
productRoutes.put('/:id', validateProductId, findProduct, validateCategories, updateProduct);
productRoutes.patch('/:id', validateProductId, findProduct, validateCategories, patchProduct);
productRoutes.delete('/:id', validateProductId, findProduct, deleteProduct);

export default productRoutes;
