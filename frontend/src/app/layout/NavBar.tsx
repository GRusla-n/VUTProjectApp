import React from 'react';
import { Button, Container, Menu } from 'semantic-ui-react';
import { useStore } from '../stores/store';

export const NavBar = () => {
  const { productStore } = useStore();
  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item header>
          <img
            src="/assets/logo.svg"
            alt="logo"
            style={{ marginRight: 'auto' }}
          />
        </Menu.Item>
        <Menu.Item name="Product" />
        <Menu.Item name="Category" />
        <Menu.Item>
          <Button
            onClick={() => productStore.openForm()}
            positive
            content="Create Product"
          />
        </Menu.Item>
      </Container>
    </Menu>
  );
};
