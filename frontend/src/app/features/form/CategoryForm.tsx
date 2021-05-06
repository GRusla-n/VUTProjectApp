import React, { useState, useEffect } from 'react';
import { Button, Container, Form, Segment } from 'semantic-ui-react';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';

const CategoryForm = () => {
  const { categoryStore } = useStore();
  const {
    selectedCategory,
    closeForm,
    createCategory,
    updateCategory,
    loading,
  } = categoryStore;
  const initialState = selectedCategory ?? {
    id: '',
    name: '',
  };

  const [category, setCategory] = useState(initialState);

  useEffect(() => {}, []);

  const handleSubmit = () => {
    category.id ? updateCategory(category) : createCategory(category);
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    setCategory({ ...category, [name]: value });
  };

  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit} autoComplete="off">
        <Container textAlign="right" style={{ marginBottom: '10px' }}>
          <Button type="button" icon="close" onClick={closeForm} />
        </Container>
        <Form.Input
          placeholder="Name"
          value={category.name}
          name="name"
          onChange={handleInputChange}
        />
        <Button
          loading={loading}
          floated="right"
          type="submit"
          content={category.id ? 'Update' : 'Create'}
        />
      </Form>
    </Segment>
  );
};

export default observer(CategoryForm);
