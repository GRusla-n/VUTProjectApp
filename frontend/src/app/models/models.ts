export interface Category {
  id: string;
  name: string;
}

export interface Producer {
  id: string;
  name: string;
  description: string;
  logo: string | Blob;
  country: string;
}

export interface Rating {
  id: string;
  point: string;
  description: string;
  productId: string;
}

export interface Product {
  id: string;
  name: string;
  image: string | Blob;
  weight: string;
  description: string;
  price: string;
  category: Category;
  producer: Producer;
  rating: Rating[];
}
