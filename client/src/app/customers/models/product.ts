import { Category } from "./category";
import { Highlights } from "./highlights";
import { Review } from "./review";
import { Specification } from "./specification";
import { Image } from "./image";

export interface Product {
  id: number;
  // skuId: string;
  // skuString: string;
  modelNumber: string;
  name: string;
  nameArabic: string;
  price: number;
  quantity: number;
  discount: number;
  description: string;
  descriptionAr: string;
  imageThumb: string;
  categoryId: number;
  available: boolean;
  brandId: number;
  brandCode: string;
  brandName: string;
  overallRating?: number;
  isFreeDelivered?: boolean;
  sellerId?: string;
  sellerName?: string;
  maxQuantityPerOrder?: number;
  highlights?: string[];
  imagesGallery?: string[];
  specifications?: Specification[];
  reviews?: Review[];
  parentsCategories?: Category[];
}
