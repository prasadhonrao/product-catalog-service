import mongoose from 'mongoose';

const categorySchema = new mongoose.Schema({
  id: { type: String, required: true },
  name: { type: String, required: true },
  description: String,
});

const Category = mongoose.model('Category', categorySchema);

export default Category;
