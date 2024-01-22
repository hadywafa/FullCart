import { Brand } from "./brand";

export interface Category {
  id: number;
  code: string;
  name: string;
  nameAr: string;
  isTop: boolean;
  parentId?: number;
  brands?: number[];
  children?: Category[];
}
