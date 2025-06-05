import React from 'react'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import HomePage from './pages/home';
import LoginPage from './pages/login';
import RegisterPage from './pages/register';
import Profile from './components/dashboard/profile';
import DashBoard from './components/dashboard/dashBoard';
import RequestBlood from './components/dashboard/requestBlood';
import { Provider } from 'react-redux';
import { persistor, store } from './redux/store';
import { PersistGate } from 'redux-persist/integration/react';


const router = createBrowserRouter([
  {
    path: "/",
    element:<HomePage/>
  },
    {
    path: "/login",
    element: <LoginPage/>
  },
    {
    path: "/register",
    element:<RegisterPage/>
  },
    {
    path: "/profile",
    element:<Profile/>
  },
   {
    path: "/request-blood",
    element:<RequestBlood/>
  },
   {
    path: "/dashboard",
    element:<DashBoard/>
  },
 
]);
function App() {
  return (
    <><Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
       <RouterProvider router={router} />
       </PersistGate>
    </Provider>
    </>
     
  
  )
}

export default App