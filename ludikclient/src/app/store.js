import { configureStore } from '@reduxjs/toolkit';
import userReducer from '../features/auth/userSlice.js';

export const store = configureStore({
  reducer: {
    user: userReducer,
  },
});