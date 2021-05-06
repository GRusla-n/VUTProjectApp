import React, { useState } from 'react';
import { Button, Container, Form, Segment } from 'semantic-ui-react';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';
import ImageUpload from '../../utils/imageUpload/imageUpload';

const ProducerForm = () => {
  const { producerStore } = useStore();
  const {
    selectedProducer,
    closeForm,
    createProducer,
    updateProducer,
    loading,
  } = producerStore;
  const initialState = selectedProducer ?? {
    id: '',
    name: '',
    description: '',
    logo: '',
    country: '',
  };

  const [producer, setProducer] = useState(initialState);

  const handleSubmit = () => {
    producer.id ? updateProducer(producer) : createProducer(producer);
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    setProducer({ ...producer, [name]: value });
  };

  const handleImageUpload = (file: Blob) => {
    setProducer({ ...producer, logo: file });
  };

  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit} autoComplete="off">
        <Container textAlign="right" style={{ marginBottom: '10px' }}>
          <Button type="button" icon="close" onClick={closeForm} />
        </Container>
        <Form.Input
          placeholder="Name"
          value={producer.name}
          name="name"
          onChange={handleInputChange}
        />
        <Form.Input
          placeholder="Country"
          value={producer.country}
          name="country"
          onChange={handleInputChange}
        />
        <Form.TextArea
          placeholder="Description"
          value={producer.description || ''}
          name="description"
          onChange={handleInputChange}
        />
        <ImageUpload loading={loading} uploadImage={handleImageUpload} />
        <Button
          loading={loading}
          floated="right"
          type="submit"
          content={producer.id ? 'Update' : 'Create'}
        />
      </Form>
    </Segment>
  );
};

export default observer(ProducerForm);
