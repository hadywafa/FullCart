import { OrderItem } from "./order-item";

export interface Order {
  id: number;
  deliveryStatus: string;
  deliveryStatusDescription: string;
  totalPrice: number;
  orderItems: OrderItem[];
}
