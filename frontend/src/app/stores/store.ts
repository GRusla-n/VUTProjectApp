import { createContext, useContext } from 'react';
import CategoryStore from './categoryStore';
import ProducerStore from './producerStore';
import ProductStore from './productStore';

interface Store {
  productStore: ProductStore;
  categoryStore: CategoryStore;
  producerStore: ProducerStore;
}

export const store: Store = {
  productStore: new ProductStore(),
  categoryStore: new CategoryStore(),
  producerStore: new ProducerStore(),
};

export const StoreContext = createContext(store);

export const useStore = () => {
  return useContext(StoreContext);
};
