import mongoose from 'mongoose';

// Middleware to validate product ID
const validateProductId = (req, res, next) => {
  const { id } = req.params;
  if (!mongoose.Types.ObjectId.isValid(id)) {
    res.status(400).json({ message: 'Invalid product ID' });
    return;
  }
  next();
};

export default validateProductId;
