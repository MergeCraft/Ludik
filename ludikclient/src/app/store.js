import { configureStore } from '@reduxjs/toolkit';
import userReducer from '../features/auth/hooks/userSlice.js';

export const store = configureStore({
  reducer: {
    userData: userReducer,
  },
});