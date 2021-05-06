import { observer } from 'mobx-react-lite';
import React from 'react';
import { Button, Item, Segment } from 'semantic-ui-react';
import { useStore } from '../../stores/store';

const Categories = () => {
  const { categoryStore } = useStore();
  const { categories } = categoryStore;

  return (
    <Segment>
      <Item.Group divided>
        {categories.map((category) => (
          <Item key={category.id}>
            <Item.Content>
              <Item.Header as="a">{category.id}</Item.Header>
              <Item.Description>{category.name}</Item.Description>
              <Item.Extra>
                <Button
                  onClick={() => categoryStore.selectCategory(category.id)}
                  floated="right"
                  content="View"
                />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(Categories);
