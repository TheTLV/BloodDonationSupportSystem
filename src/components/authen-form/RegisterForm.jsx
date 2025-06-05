import { Button, Checkbox, Form, Input } from 'antd';
import React from 'react'
import api from '../../configs/axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

function RegisterForm() {
  const navigate = useNavigate();
  const onFinish = async( values) => {
  console.log('Success:', values);
  // value = thong tin ng dung nhap
  // 400 :pass request
  // 200: thanh cong
  try {
    await api.post('register', values)
    toast.success('Register successfully, please login!');
    navigate('/login');
  } catch (e) {
    // show cho nguoi dung loi
    console.log(e)
    toast.error(e.response.data);
  }
};
const onFinishFailed = errorInfo => {
  console.log('Failed:', errorInfo);
};
  return (
    <div className='register-form'>
        <h1>Register</h1>
    <Form
  name="basic"
  layout="vertical"
  labelCol={{ span: 24 }}
  style={{ maxWidth: 600 }}
  initialValues={{ remember: true }}
  onFinish={onFinish}
  onFinishFailed={onFinishFailed}
  autoComplete="off"
>
  <Form.Item
    label="Full Name"
    name="fullName"
    rules={[{ required: true, message: 'Please input your full name!' }]}
  >
    <Input />
  </Form.Item>

  <Form.Item
    label="Email"
    name="email"
    rules={[
      { required: true, message: 'Please input your email!' },
      { type: 'email', message: 'Please enter a valid email!' },
    ]}
  >
    <Input />
  </Form.Item>

  <Form.Item
    label="Username"
    name="username"
    rules={[{ required: true, message: 'Please input your username!' }]}
  >
    <Input />
  </Form.Item>

  <Form.Item
    label="Password"
    name="password"
    rules={[{ required: true, message: 'Please input your password!' }]}
    hasFeedback
  >
    <Input.Password />
  </Form.Item>

  <Form.Item
    label="Confirm Password"
    name="confirmPassword"
    dependencies={['password']}
    hasFeedback
    rules={[
      { required: true, message: 'Please confirm your password!' },
      ({ getFieldValue }) => ({
        validator(_, value) {
          if (!value || getFieldValue('password') === value) {
            return Promise.resolve();
          }
          return Promise.reject(new Error('The two passwords do not match!'));
        },
      }),
    ]}
  >
    <Input.Password />
  </Form.Item>

  <Form.Item name="remember" valuePropName="checked">
    <Checkbox>Remember me</Checkbox>
  </Form.Item>

  <Form.Item>
    <Button type="primary" htmlType="submit">
      Submit
    </Button>
  </Form.Item>
</Form>

    </div>
  )
}

export default RegisterForm