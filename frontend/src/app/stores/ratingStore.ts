import { makeAutoObservable } from 'mobx';
import agent from '../api/agent';
import { Rating } from '../models/models';

export default class CategoryStore {
  categories: Rating[] = [];
  selectedCategory: Rating | undefined = undefined;

  constructor() {
    makeAutoObservable(this);
  }
}
