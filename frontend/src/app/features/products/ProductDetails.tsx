import React, { useState } from 'react';
import { Card, Image, Button } from 'semantic-ui-react';
import { LoadingComponent } from '../../layout/LoadingComponents';
import { useStore } from '../../stores/store';
import { SyntheticEvent } from 'react-dom/node_modules/@types/react';

export const ProductDetail = () => {
  const { productStore } = useStore();
  const {
    selectedProduct: product,
    openForm,
    loading,
    deleteProduct,
  } = productStore;
  const [target, setTarget] = useState('');

  const handleProductDelete = (
    e: SyntheticEvent<HTMLButtonElement>,
    id: string,
  ) => {
    setTarget(e.currentTarget.name);
    deleteProduct(id);
  };

  if (!product) return <LoadingComponent />;

  return (
    <Card>
      <Image src={product.image} wrapped ui={false} />
      <Card.Content>
        <Card.Header>{product.name}</Card.Header>
        <Card.Meta>
          <span>{`$${product.price}`}</span>
        </Card.Meta>
        <Card.Description>{product.description}</Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button.Group widths="2">
          <Button
            name={product.id}
            loading={loading && target === product.id}
            onClick={(e) => handleProductDelete(e, product.id)}
            floated="right"
            content="Delete"
            basic
          />
          <Button onClick={() => openForm(product.id)} content="Edit" />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};
