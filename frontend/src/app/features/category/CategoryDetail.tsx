import React, { useState } from 'react';
import { Card, Image, Button } from 'semantic-ui-react';
import { LoadingComponent } from '../../layout/LoadingComponents';
import { useStore } from '../../stores/store';
import { SyntheticEvent } from 'react-dom/node_modules/@types/react';

export const CategoryDetail = () => {
  const { categoryStore } = useStore();
  const {
    selectedCategory: category,
    openForm,
    loading,
    deleteCategory,
  } = categoryStore;
  const [target, setTarget] = useState('');

  const handleProductDelete = (
    e: SyntheticEvent<HTMLButtonElement>,
    id: string,
  ) => {
    setTarget(e.currentTarget.name);
    deleteCategory(id);
  };

  if (!category) return <LoadingComponent />;

  return (
    <Card>
      <Card.Content>
        <Card.Header>{category.name}</Card.Header>
      </Card.Content>
      <Card.Content extra>
        <Button.Group widths="2">
          <Button
            name={category.id}
            loading={loading && target === category.id}
            onClick={(e) => handleProductDelete(e, category.id)}
            floated="right"
            content="Delete"
            basic
          />
          <Button onClick={() => openForm(category.id)} content="Edit" />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};
