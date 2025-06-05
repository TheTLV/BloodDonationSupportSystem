import { createSlice } from '@reduxjs/toolkit';

const initialState = null; // hoặc {} nếu bạn muốn trạng thái mặc định là object

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    login: (state, action) => {
      return action.payload;
    },
    logout: () => {
      return initialState;
    }
  }
});

// Đây là destructuring chuẩn — không lỗi nếu userSlice định nghĩa đúng
export const { login, logout } = userSlice.actions;

export default userSlice.reducer;
