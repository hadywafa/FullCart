import { DeliveryStatus } from "./delivery-status";
import { Product } from "./product";

export interface OrderDetails {
  id: number;
  deliveryStatus: DeliveryStatus;
  deliveryStatusDescription: string;
  totalPrice: number;
  products: Product[];
}
