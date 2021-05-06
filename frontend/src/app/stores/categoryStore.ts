import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Category } from '../models/models';

export default class CategoryStore {
  categories: Category[] = [];
  selectedCategory: Category | undefined = undefined;
  editMode = false;
  loading = false;

  constructor() {
    makeAutoObservable(this);
  }

  loadCategory = async () => {
    try {
      const products = await agent.Categories.list();
      products.forEach((category) => this.categories.push(category));
    } catch (error) {
      console.log(error);
    }
  };

  createCategory = async (category: Category) => {
    this.loading = true;
    try {
      await agent.Categories.create(category);
      runInAction(async () => {
        this.categories.push(category);
        this.selectedCategory = category;
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

  updateCategory = async (category: Category) => {
    this.loading = true;
    try {
      await agent.Categories.update(category);
      runInAction(async () => {
        this.categories = [
          ...this.categories.filter((x) => x.id !== category.id),
          category,
        ];
        this.selectedCategory = category;
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

  deleteCategory = async (id: string) => {
    this.loading = true;
    try {
      await agent.Categories.delete(id);
      runInAction(() => {
        this.categories = [...this.categories.filter((x) => x.id !== id)];
        this.selectedCategory = undefined;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  selectCategory = (id: string) => {
    this.selectedCategory = this.categories.find((a) => a.id === id);
  };

  cancelSelectedCategory = () => {
    this.selectedCategory = undefined;
  };

  openForm = (id?: string) => {
    id ? this.selectCategory(id) : this.cancelSelectedCategory();
    this.editMode = true;
  };

  closeForm = () => {
    this.editMode = false;
  };
}
