import React from 'react';
import { Card, Image, Button } from 'semantic-ui-react';
import { LoadingComponent } from '../../layout/LoadingComponents';
import { useStore } from '../../stores/store';

export const ProductDetail = () => {
  const { productStore } = useStore();
  const {
    selectedProduct: product,
    openForm,
    cancelSelectedProduct,
  } = productStore;

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
          <Button onClick={() => openForm(product.id)} basic content="Edit" />
          <Button
            onClick={() => cancelSelectedProduct()}
            basic
            content="Cancel"
          />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};
