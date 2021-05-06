import React, { useEffect } from 'react';
import './styles.css';
import NavBar from './NavBar';
import { Container } from 'semantic-ui-react';
import Products from '../features/products/Products';
import { LoadingComponent } from './LoadingComponents';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';
import { Route } from 'react-router';
import Categories from '../features/category/Categories';
import Producers from '../features/producer/Producers';
import HomePage from '../features/HomePage';

function App() {
  const { productStore, categoryStore, producerStore } = useStore();

  useEffect(() => {
    productStore.loadProducts();
    categoryStore.loadCategory();
    producerStore.loadProducer();
  }, [productStore, categoryStore, producerStore]);

  if (productStore.loadingInitial)
    return <LoadingComponent content="Loading app" />;

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '100px' }}>
        <Route path="/" component={HomePage} exact />
        <Route path="/product" component={Products} />
        <Route path="/category" component={Categories} />
        <Route path="/producer" component={Producers} />
      </Container>
    </>
  );
}

export default observer(App);
