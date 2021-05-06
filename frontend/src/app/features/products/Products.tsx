import React from 'react';
import { Grid } from 'semantic-ui-react';
import { useStore } from '../../stores/store';
import ProductForm from '../form/ProductForm';
import { ProductDetail } from './ProductDetails';
import ProductList from './ProductList';
import { observer } from 'mobx-react-lite';

const Products = () => {
  const { productStore } = useStore();
  const { selectedProduct, editMode } = productStore;
  return (
    <Grid>
      <Grid.Column width="10">
        <ProductList />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedProduct && !editMode && <ProductDetail />}
        {editMode && <ProductForm />}
      </Grid.Column>
    </Grid>
  );
};

export default observer(Products);
