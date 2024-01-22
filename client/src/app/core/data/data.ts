import { Category } from "../../customers/models/category";
import { Order } from "../../customers/models/order";
import { Product } from "../../customers/models/product";
import { User } from "./../../customers/models/user";
export interface appInMemoryWebApiData {
  users: User[];
  products: Product[];
  orders: Order[];
  categories: Category[];
  subCategories: Category[];
}
