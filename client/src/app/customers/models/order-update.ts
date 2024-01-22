import { DeliveryStatus } from "./delivery-status";

export interface OrderUpdate {
  id: number;
  deliveryStatus: DeliveryStatus;
  totalPrice: number;
  orderDate: any;
}
