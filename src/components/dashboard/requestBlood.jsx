import React, { useState } from 'react';
import { Table, Input, Button, Space, Select } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { title } from 'framer-motion/client';

const originalData = [
  {
    key: '1',
    name: 'Bill',
    age: 30,
    typeBlood: 'A+',
    phonenumber: '123-456-789',
    email:'Bill@gmail.com',
    address: '123 Main St, City, Country',
    status: 'pending',
  },
  {
    key: '2',
    name: 'Tom',
    age: 30,
    typeBlood: 'O+',
    phonenumber: '987-654-321',
    email:'Tom@gmail.com',
    address: '123 Main St, City, Country',
    status: 'pending',
  },
];

const columns = [
  { title: 'Name', dataIndex: 'name', key: 'name' },
  { title: 'Age', dataIndex: 'age', key: 'age' },
  { title: 'Request Blood', dataIndex: 'typeBlood', key: 'typeBlood' },
  { title: 'Phone Number', dataIndex: 'phonenumber', key: 'phonenumber' },
  {title:'Email', dataIndex: 'email', key: 'email'},
  { title: 'Address', dataIndex: 'address', key: 'address' },
  {
    title: 'Status',
    key: 'status',
    render: (_, record) => (
      <Select
        defaultValue={record.status || 'pending'}
        style={{ width: 120 }}
        onChange={(value) => handleStatusChange(record.key, value)}
      >
        <Option value="pending">Accepted</Option>
        <Option value="approved">Refuse</Option>
        <Option value="rejected">Processing</Option>
      </Select>
    ),
  },
];

const RequestBlood = () => {
  const [searchText, setSearchText] = useState('');
  const [filteredData, setFilteredData] = useState(originalData);

  const handleSearch = () => {
    const filtered = originalData.filter(item =>
      item.typeBlood.toLowerCase().includes(searchText.toLowerCase())
    );
    setFilteredData(filtered);
  };

  const handleReset = () => {
    setSearchText('');
    setFilteredData(originalData);
  };
  const handleStatusChange = (key, newStatus) => {
  console.log(`Status of record ${key} changed to: ${newStatus}`);
  // mot gan link toi API de cap nhat trang thai
};

  return (
    <div style={{ padding: 24 }}>
      <Space style={{ marginBottom: 16 }}>
        <Input
          placeholder="Search by Tpe of Blood"
          value={searchText}
          onChange={e => setSearchText(e.target.value)}
          onPressEnter={handleSearch}
        />
        <Button type="primary" icon={<SearchOutlined />} onClick={handleSearch}>
          Search
        </Button>
      </Space>
      <Table dataSource={filteredData} columns={columns} />
    </div>
  );
};

export default RequestBlood;
