import React, { useState } from 'react';
import {
  DesktopOutlined,
  FileOutlined,
  LogoutOutlined,
  PieChartOutlined,
  TeamOutlined,
  UserOutlined,
} from '@ant-design/icons';
import { Avatar, Breadcrumb, Button, Layout, Menu, theme } from 'antd';
import Profile from './profile';
import RequestBlood from './requestBlood';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { logout } from '../../redux/features/userSlice';

const { Header, Content, Footer, Sider } = Layout;

function getItem(label, key, icon, children) {
  
  return {
    key,
    icon,
    children,
    label,
  };
}

const items = [
  getItem('Option 1', '1', <PieChartOutlined />),
  getItem('Option 2', '2', <DesktopOutlined />),
  getItem('User', 'sub1', <UserOutlined />, [
    getItem('Member', '3'),
    getItem('Staff', '4'),
    getItem('Alex', '5'),
  ]),
  getItem('Search', 'sub2', <TeamOutlined />, [
    getItem('Donor', '6'),
    getItem('Patient', '8'),
  ]),
  getItem('Blogs', '9', <FileOutlined />),
  getItem('Event', '12', <PieChartOutlined />),
  getItem('Contact', '10', <PieChartOutlined />),
  getItem('Feed back', '11', <PieChartOutlined />),
];

const DashBoard = () => {
  const [collapsed, setCollapsed] = useState(false);
  const [selectedKey, setSelectedKey] = useState('1');

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  const renderContent = () => {
    switch (selectedKey) {
      case '1':
        return <h2>Welcome to Staff Dashboard Overview</h2>;
      case '2':
        return <h2>This is Option 2 content</h2>;
      case '3':
        return <Profile/>;
      case '4':
        return <h2>Bill is a cat.</h2>;
      case '5':
        return <h2>Alex is a doctor.</h2>;
      case '6':
        return <RequestBlood/>
      case '8':
        return <h2>Team 2 Information</h2>;
      case '9':
        return <h2>Files and Documents</h2>;
      default:
        return <h2>Welcome!</h2>;
    }
  };
const user = useSelector((state)=> state.user)
const dispatch = useDispatch();
const navigate = useNavigate();

const handleLogout = () => {
  dispatch(logout());
  localStorage.removeItem('token');
  navigate('/login');
};
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider collapsible collapsed={collapsed} onCollapse={value => setCollapsed(value)}>
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['1']}
          onClick={({ key }) => setSelectedKey(key)}
          items={items}
        />
      </Sider>
      <Layout>
        <Header
          style={{
            padding: '0 24px',
            background: colorBgContainer,
            display: 'flex',
            justifyContent: 'flex-end',
            alignItems: 'center',
          }}
        >
          <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
            <span style={{ fontWeight: 'bold' }}>{user?.fullName}</span>
            <Avatar src="https://i.pravatar.cc/300" alt="avatar" />
          </div>
          <div><Button style={{ margin:'10px '}} onClick={handleLogout} > Logout </Button></div>
        </Header>

        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb
            style={{ margin: '16px 0' }}
            items={[{ title: 'Menu' }, { title: selectedKey }]}
          />
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            {renderContent()}
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};

export default DashBoard;
