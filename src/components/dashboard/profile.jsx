import React from 'react';
import { Table } from 'antd';

const dataSource = [
  {
    key: '1',
    name: 'Bill',
    username: 'billyyy tyyy',
    phoneNumber: '123-456-789',
    email:'bill@gmail.com'  ,
  },
  {
    key: '2',
    name: 'Tom',
    username: 'tomm holland',
    phoneNumber: '987-654-321',
    email:'Tom@gmail.com' ,
  },
  {
    key: '3',
    name: 'Jerry',
    username: 'jerry mouse',
    phoneNumber: '456-789-123',
    email:'Jerry@gmail.com'
  },
]
const columns = [
  { title: 'Name', dataIndex: 'name', key: 'name' },
  { title: 'Phone Number', dataIndex: 'phoneNumber', key: 'phoneNumber' },
  { title: 'Username', dataIndex: 'username', key: 'username' },
  { title: 'Email', dataIndex: 'email', key: 'email' },
];

const Profile = () => {
  return (
    <div style={{ padding: 24 }}>
      <Table dataSource={dataSource} columns={columns} />
    </div>
  );
};

export default Profile;
