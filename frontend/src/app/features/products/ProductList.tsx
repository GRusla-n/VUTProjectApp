import { observer } from 'mobx-react-lite';
import { useState } from 'react';
import { SyntheticEvent } from 'react-dom/node_modules/@types/react';
import { Button, Item, Label, Segment } from 'semantic-ui-react';
import { useStore } from '../../stores/store';

const ProductList = () => {
  const { productStore } = useStore();
  const { products, loading, deleteProduct } = productStore;

  const [target, setTarget] = useState('');

  const handleProductDelete = (
    e: SyntheticEvent<HTMLButtonElement>,
    id: string,
  ) => {
    setTarget(e.currentTarget.name);
    deleteProduct(id);
  };

  return (
    <Segment>
      <Item.Group divided>
        {products.map((product) => (
          <Item key={product.id}>
            <Item.Content>
              <Item.Header as="a">{product.name}</Item.Header>
              <Item.Meta>{product.price}</Item.Meta>
              <Item.Description>{product.description}</Item.Description>
              <Item.Extra>
                <Button
                  onClick={() => productStore.selectProduct(product.id)}
                  floated="right"
                  content="View"
                />
                <Button
                  name={product.id}
                  loading={loading && target === product.id}
                  onClick={(e) => handleProductDelete(e, product.id)}
                  floated="right"
                  content="Delete"
                />
                <Label basic content={product.category.name} />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(ProductList);
