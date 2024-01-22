import { Rate } from "./rate.enum";

export interface Review {
  id: number;
  productId: number;
  // customerId: number;
  customerName: string;
  SellerName: string;
  productRating: Rate;
  sellerRating: Rate;
  productComment: string;
  sellerComment: string;
  isProductCommentAnonymous: boolean;
  isSellerCommentAnonymous: boolean;
  isAsDescription: boolean;
  isDeliveredOnTime: boolean;
  createdAt: Date;
}
