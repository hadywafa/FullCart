import { Review } from "./review";

export interface Seller {
  id: number;
  name: string;
  openSince: Date;
  ratesOneCount: number;
  ratesTwoCount: number;
  ratesThreeCount: number;
  ratesFourCount: number;
  ratesFiveCount: number;
  TotalRating: number;
  sellerRating: number;
  customerCount: number;
  Reviews: Review[];
}
