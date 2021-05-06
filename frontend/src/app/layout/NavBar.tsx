import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';
import { Container, Menu } from 'semantic-ui-react';

const NavBar = () => {
  const [activeItem, setActiveItem] = useState('');
  const handleItemClick = (e: any, { name }: any) => setActiveItem(name);
  const menuItemStyle = {
    padding: 0,
  };
  const navLinkStyle = {
    padding: '10px 13px 10px 13px',
  };

  return (
    <Menu tabular>
      <Container>
        <Menu.Item header>
          <NavLink to="/">
            <img
              src="/assets/logo.svg"
              alt="logo"
              style={{ marginRight: 'auto', width: '5.5em' }}
            />
          </NavLink>
        </Menu.Item>
        <Menu.Item
          name="product"
          onClick={handleItemClick}
          active={activeItem === 'product'}
          style={{ ...menuItemStyle }}
        >
          <NavLink to="/product" style={{ ...navLinkStyle }}>
            Products
          </NavLink>
        </Menu.Item>
        <Menu.Item
          name="category"
          onClick={handleItemClick}
          active={activeItem === 'category'}
          style={{ ...menuItemStyle }}
        >
          <NavLink to="/category" style={{ ...navLinkStyle }}>
            Category
          </NavLink>
        </Menu.Item>
        <Menu.Item
          name="producer"
          onClick={handleItemClick}
          active={activeItem === 'producer'}
          style={{ ...menuItemStyle }}
        >
          <NavLink to="/producer" style={{ ...navLinkStyle }}>
            Producer
          </NavLink>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
