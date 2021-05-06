import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Product } from '../models/models';

export default class ProductStore {
  products: Product[] = [];
  selectedProduct: Product | undefined = undefined;
  editMode = false;
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  setLoadingInitial = (state: boolean) => {
    this.loadingInitial = state;
  };

  loadProducts = async () => {
    this.setLoadingInitial(true);
    try {
      const products = await agent.Products.list();
      products.forEach((product) => this.products.push(product));
      this.setLoadingInitial(false);
    } catch (error) {
      this.setLoadingInitial(false);
      console.log(error);
    }
  };

  selectProduct = (id: string) => {
    this.selectedProduct = this.products.find((a) => a.id === id);
  };

  cancelSelectedProduct = () => {
    this.selectedProduct = undefined;
  };

  openForm = (id?: string) => {
    id ? this.selectProduct(id) : this.cancelSelectedProduct();
    this.editMode = true;
  };

  closeForm = () => {
    this.editMode = false;
  };

  createProduct = async (product: Product) => {
    this.loading = true;
    try {
      let productFromRepo = await agent.Products.create(product);
      runInAction(async () => {
        productFromRepo = await agent.Products.details(productFromRepo.id);
        this.products.push(productFromRepo);
        this.selectedProduct = productFromRepo;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      console.error(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  updateProduct = async (product: Product) => {
    this.loading = true;
    try {
      await agent.Products.update(product);
      runInAction(async () => {
        var productFromRepo = await agent.Products.details(product.id);
        this.products = [
          ...this.products.filter((x) => x.id !== product.id),
          productFromRepo,
        ];
        this.selectedProduct = productFromRepo;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  deleteProduct = async (id: string) => {
    this.loading = true;
    try {
      await agent.Products.delete(id);
      runInAction(() => {
        this.products = [...this.products.filter((x) => x.id !== id)];
        this.loading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };
}
