import React from 'react';
import { Button, Grid } from 'semantic-ui-react';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';
import CategoryList from './CategoryList';
import CategoryForm from '../form/CategoryForm';
import { CategoryDetail } from './CategoryDetail';

const Products = () => {
  const { categoryStore } = useStore();
  const { selectedCategory, editMode, openForm } = categoryStore;
  return (
    <Grid>
      <Grid.Row style={{ justifyContent: 'right' }}>
        <Button onClick={() => openForm()} positive content="Create Caregory" />
      </Grid.Row>
      <Grid.Row>
        <Grid.Column width="10">
          <CategoryList />
        </Grid.Column>
        <Grid.Column width="6">
          {selectedCategory && !editMode && <CategoryDetail />}
          {editMode && <CategoryForm />}
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(Products);
