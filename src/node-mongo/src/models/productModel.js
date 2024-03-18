import mongoose from 'mongoose';
const { Schema } = mongoose;

const productSchema = new mongoose.Schema({
  id: { type: String },
  productName: { type: String, required: true },
  productDescription: { type: String, required: true },
  price: { type: Number, required: true },
  quantity: { type: Number, required: true },
  status: { type: String, default: 'In Stock' },
  inventoryStatus: String,
  seo: {
    metaTitle: String,
    metaDescription: String,
    slug: String,
  },
  categories: [{ type: Schema.Types.ObjectId, ref: 'Category' }],
  specifications: [
    {
      name: String,
      value: String,
    },
  ],
  images: [
    {
      imageUrl: String,
      imageDescription: String,
    },
  ],
  variants: [
    {
      id: String,
      color: String,
      size: String,
      quantity: Number,
    },
  ],
  relatedProducts: [String],
  reviews: [
    {
      customerId: String,
      comment: String,
    },
  ],
  ratings: [
    {
      customerId: String,
      rating: Number,
    },
  ],
  AggregateRating: Number,
  ReviewCount: Number,
});

// Define index for productName
productSchema.index({ productName: 'text' });

const Product = mongoose.model('Product', productSchema);

export default Product;
