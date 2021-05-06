import React, { useEffect } from 'react';
import './styles.css';
import { NavBar } from './NavBar';
import { Container } from 'semantic-ui-react';
import Products from '../features/products/Products';
import { LoadingComponent } from './LoadingComponents';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';

function App() {
  const { productStore, categoryStore } = useStore();

  useEffect(() => {
    productStore.loadProducts();
    categoryStore.loadCategory();
  }, [productStore, categoryStore]);

  if (productStore.loadingInitial)
    return <LoadingComponent content="Loading app" />;

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '100px' }}>
        <Products />
      </Container>
    </>
  );
}

export default observer(App);
