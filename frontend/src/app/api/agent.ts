import axios, { AxiosResponse } from 'axios';
import { Category, Producer, Product, Rating } from '../models/models';

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = 'http://localhost:5000/api';

// axios.interceptors.response.use((response) => {
//   return sleep(1000)
//     .then(() => {
//       return response;
//     })
//     .catch((error) => {
//       console.log(error);
//       return Promise.reject(error);
//     });
// });

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}, header?: {}) =>
    axios.post<T>(url, body, header).then(responseBody),
  put: <T>(url: string, body: {}, header?: {}) =>
    axios.put<T>(url, body, header).then(responseBody),
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

    return requests.post<Product>(`/product/`, formData, {
      headers: { 'Content-type': 'multipart/form-data' },
    });
  },
  update: (product: Product) => {
    let formData = new FormData();
    formData.append('Name', product.name);
    formData.append('Image', product.image);
    formData.append('Weight', product.weight);
    formData.append('Description', product.description);
    formData.append('Price', product.price);
    formData.append('CategoryId', product.category.id);
    formData.append('ProducerId', product.producer.id);

    return requests.put<Product>(`/product/${product.id}`, formData, {
      headers: { 'Content-type': 'multipart/form-data' },
    });
  },
  delete: (id: string) => requests.delete(`/product/${id}`),
};

const Producers = {
  list: () => requests.get<Producer[]>('/producer'),
  details: (id: string) => requests.get<Producer>(`/producer/${id}`),
  create: (producer: Producer) => {
    let formData = new FormData();
    formData.append('Name', producer.name);
    formData.append('Description', producer.description);
    formData.append('Logo', producer.logo);
    formData.append('Country', producer.country);

    return requests.post<Producer>(`/producer/`, formData, {
      headers: { 'Content-type': 'multipart/form-data' },
    });
  },
  update: (producer: Producer) => {
    let formData = new FormData();
    formData.append('Name', producer.name);
    formData.append('Description', producer.description);
    formData.append('Logo', producer.logo);
    formData.append('Country', producer.country);

    return requests.put<Producer>(`/producer/${producer.id}`, formData, {
      headers: { 'Content-type': 'multipart/form-data' },
    });
  },
  delete: (id: string) => requests.delete(`/producer/${id}`),
};

const Categories = {
  list: () => requests.get<Category[]>('/category/'),
  details: (id: string) => requests.get<Category>(`/category/${id}`),
  create: (category: Category) =>
    requests.post<Category>('/category/', category),
  update: (category: Category) =>
    requests.put<Category>(`/category/${category.id}`, category),
  delete: (id: string) => requests.delete(`/category/${id}`),
};

const Ratings = {
  list: () => requests.get<Rating[]>('/rating/'),
  create: (rating: Rating) => {
    requests.post<Rating>('/rating/', rating);
  },
};

const agent = {
  Products,
  Categories,
  Ratings,
  Producers,
};

export default agent;
