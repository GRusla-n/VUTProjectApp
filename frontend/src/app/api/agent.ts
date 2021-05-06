import axios, { AxiosResponse } from 'axios';
import { Category, Product } from '../models/models';

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use((response) => {
  return sleep(1000)
    .then(() => {
      return response;
    })
    .catch((error) => {
      console.log(error);
      return Promise.reject(error);
    });
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios
      .post<T>(url, body, {
        headers: { 'Content-type': 'multipart/form-data' },
      })
      .then(responseBody),
  put: <T>(url: string, body: {}) =>
    axios
      .put<T>(url, body, {
        headers: { 'Content-type': 'multipart/form-data' },
      })
      .then(responseBody),
  delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Products = {
  list: () => requests.get<Product[]>('/product'),
  details: (id: string) => requests.get<Product>(`/product/${id}`),
  create: (product: Product) => {
    let formData = new FormData();
    formData.append('Name', product.name);
    formData.append('Image', product.image);
    formData.append('Weight', product.weight);
    formData.append('Description', product.description);
    formData.append('Price', product.price);
    formData.append('CategoryId', product.category.id);
    formData.append('ProducerId', product.producer.id);

    return requests.post<Product>(`/product/`, formData);
  },
  update: (product: Product) => {
    let formData = new FormData();
    formData.append('Name', product.name);
    formData.append('Weight', product.weight);
    formData.append('Description', product.description);
    formData.append('Price', product.price);
    formData.append('CategoryId', product.category.id);
    formData.append('ProducerId', product.producer.id);

    return requests.put(`/product/${product.id}`, formData);
  },
  delete: (id: string) => requests.delete(`/product/${id}`),
};

const Categories = {
  list: () => requests.get<Category[]>('/category/'),
};

const agent = {
  Products,
  Categories,
};

export default agent;
