import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Producer } from '../models/models';

export default class ProducerStore {
  producers: Producer[] = [];
  selectedProducer: Producer | undefined = undefined;
  editMode = false;
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  setLoadingInitial = (state: boolean) => {
    this.loadingInitial = state;
  };

  loadProducer = async () => {
    this.setLoadingInitial(true);
    try {
      const producers = await agent.Producers.list();
      producers.forEach((producer) => this.producers.push(producer));
      this.setLoadingInitial(false);
    } catch (error) {
      this.setLoadingInitial(false);
      console.log(error);
    }
  };

  selectProducer = (id: string) => {
    this.selectedProducer = this.producers.find((a) => a.id === id);
  };

  cancelSelectedProducer = () => {
    this.selectedProducer = undefined;
  };

  openForm = (id?: string) => {
    id ? this.selectProducer(id) : this.cancelSelectedProducer();
    this.editMode = true;
  };

  closeForm = () => {
    this.editMode = false;
  };

  createProducer = async (producer: Producer) => {
    this.loading = true;
    try {
      let producerFromRepo = await agent.Producers.create(producer);
      runInAction(async () => {
        producerFromRepo = await agent.Producers.details(producerFromRepo.id);
        this.producers.push(producerFromRepo);
        this.selectedProducer = producerFromRepo;
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

  updateProducer = async (producer: Producer) => {
    this.loading = true;
    try {
      await agent.Producers.update(producer);
      runInAction(async () => {
        var producerFromRepo = await agent.Producers.details(producer.id);
        this.producers = [
          ...this.producers.filter((x) => x.id !== producer.id),
          producerFromRepo,
        ];
        this.selectedProducer = producerFromRepo;
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

  deleteProducer = async (id: string) => {
    this.loading = true;
    try {
      await agent.Producers.delete(id);
      runInAction(() => {
        this.producers = [...this.producers.filter((x) => x.id !== id)];
        this.loading = false;
        this.selectedProducer = undefined;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };
}
