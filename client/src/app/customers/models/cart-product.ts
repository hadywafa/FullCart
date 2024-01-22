import { Product } from "./product";
import { User } from "./user";

export interface CartProduct {
  quantity: number;
  customer: User;
  product: Product;
}
