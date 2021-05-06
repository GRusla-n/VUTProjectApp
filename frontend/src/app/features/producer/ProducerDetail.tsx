import React, { useState } from 'react';
import { Card, Image, Button } from 'semantic-ui-react';
import { LoadingComponent } from '../../layout/LoadingComponents';
import { useStore } from '../../stores/store';
import { SyntheticEvent } from 'react-dom/node_modules/@types/react';

export const ProducerDetail = () => {
  const { producerStore } = useStore();
  const {
    selectedProducer: producer,
    openForm,
    loading,
    deleteProducer,
  } = producerStore;
  const [target, setTarget] = useState('');

  const handleProductDelete = (
    e: SyntheticEvent<HTMLButtonElement>,
    id: string,
  ) => {
    setTarget(e.currentTarget.name);
    deleteProducer(id);
  };

  if (!producer) return <LoadingComponent />;

  return (
    <Card>
      <Image src={producer.logo} wrapped ui={false} />
      <Card.Content>
        <Card.Header>{producer.name}</Card.Header>
        <Card.Meta>
          <span>{producer.country}</span>
        </Card.Meta>
        <Card.Description>{producer.description}</Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button.Group widths="2">
          <Button
            name={producer.id}
            loading={loading && target === producer.id}
            onClick={(e) => handleProductDelete(e, producer.id)}
            floated="right"
            content="Delete"
            basic
          />
          <Button onClick={() => openForm(producer.id)} content="Edit" />
        </Button.Group>
      </Card.Content>
    </Card>
  );
};
