import { createContext, useContext } from 'react';
import CategoryStore from './categoryStore';
import ProductStore from './productStore';

interface Store {
  productStore: ProductStore;
  categoryStore: CategoryStore;
}

export const store: Store = {
  productStore: new ProductStore(),
  categoryStore: new CategoryStore(),
};

export const StoreContext = createContext(store);

export const useStore = () => {
  return useContext(StoreContext);
};
