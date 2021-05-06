import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState, useEffect } from 'react';
import { Button, Container, Form, Segment } from 'semantic-ui-react';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';
import ImageUpload from '../../utils/imageUpload/imageUpload';

const ProductForm = () => {
  const { productStore } = useStore();
  const {
    selectedProduct,
    closeForm,
    createProduct,
    updateProduct,
    loading,
  } = productStore;
  const initialState = selectedProduct ?? {
    id: '',
    name: '',
    image: '',
    weight: '0',
    description: '0',
    price: '0',
    category: {
      id: '',
      name: '',
    },
    producer: {
      id: '',
      name: '',
    },
    rating: [
      {
        id: '',
        point: '',
        description: '',
      },
    ],
  };

  const [product, setProduct] = useState(initialState);

  useEffect(() => {}, []);

  const handleSubmit = () => {
    product.id ? updateProduct(product) : createProduct(product);
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let productForId = product;
    if (name === 'category') {
      productForId.category.id = value;
      setProduct({ ...product, ...productForId });
    } else if (name === 'producer') {
      productForId.producer.id = value;
      setProduct({ ...product, ...productForId });
    } else setProduct({ ...product, [name]: value });
  };

  const handleImageUpload = (file: Blob) => {
    setProduct({ ...product, image: file });
  };

  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit} autoComplete="off">
        <Container textAlign="right" style={{ marginBottom: '10px' }}>
          <FontAwesomeIcon icon={faTimes} onClick={closeForm} />
        </Container>
        <Form.Input
          placeholder="Name"
          value={product.name}
          name="name"
          onChange={handleInputChange}
        />
        <Form.TextArea
          placeholder="Description"
          value={product.description || ''}
          name="description"
          onChange={handleInputChange}
        />
        <Form.Input
          placeholder="CategoryId"
          value={product.category.id}
          name="category"
          onChange={handleInputChange}
        />
        <Form.Input
          placeholder="ProducerId"
          value={product.producer.id}
          name="producer"
          onChange={handleInputChange}
        />
        <ImageUpload loading={loading} uploadImage={handleImageUpload} />
        <Button
          loading={loading}
          floated="right"
          type="submit"
          content="Create"
        />
      </Form>
    </Segment>
  );
};

export default observer(ProductForm);
