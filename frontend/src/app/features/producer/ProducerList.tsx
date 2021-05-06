import { observer } from 'mobx-react-lite';
import React from 'react';
import { Button, Item, Label, Segment } from 'semantic-ui-react';
import { useStore } from '../../stores/store';

const ProducerList = () => {
  const { producerStore } = useStore();
  const { producers, selectProducer } = producerStore;

  return (
    <Segment>
      <Item.Group divided>
        {producers.map((producer) => (
          <Item key={producer.id}>
            <Item.Content>
              <Item.Header as="a">{`#${producer.id} ${producer.name}`}</Item.Header>
              <Item.Description>{producer.description}</Item.Description>
              <Item.Extra>
                <Button
                  onClick={() => selectProducer(producer.id)}
                  floated="right"
                  content="View"
                />
                <Label basic content={producer.country} />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
};

export default observer(ProducerList);
