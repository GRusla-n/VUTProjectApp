import { observer } from 'mobx-react-lite';
import React from 'react';
import {
  Button,
  Item,
  Label,
  Segment,
  Rating,
  Accordion,
  Icon,
} from 'semantic-ui-react';
import { useStore } from '../../stores/store';

const ProductList = () => {
  const { productStore } = useStore();
  const { products } = productStore;

  return (
    <Segment>
      <Item.Group divided>
        {products.map((product) => (
          <Item key={product.id}>
            <Item.Content>
              <Item.Header as="a">{product.name}</Item.Header>
              <Item.Meta>{product.price}</Item.Meta>
              <Item.Description>{product.description}</Item.Description>
              <Accordion>
                <Accordion.Title>
                  <Icon name="dropdown" />
                  <Rating maxRating={5} />
                </Accordion.Title>
                <Accordion.Content>Some text</Accordion.Content>
              </Accordion>
              <Item.Extra>
                <Button
                  onClick={() => productStore.selectProduct(product.id)}
                  floated="right"
                  content="View"
                />
                <Label basic content={product.category.name} />
                <Label basic content={product.producer.name} />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(ProductList);
