import { makeAutoObservable } from 'mobx';
import agent from '../api/agent';
import { Category } from '../models/models';

export default class CategoryStore {
  categories: Category[] = [];
  selectedCategory: Category | undefined = undefined;

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

  selectCategory = (id: string) => {
    this.selectedCategory = this.categories.find((a) => a.id === id);
  };
}
