import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Order } from "../models/order";
import { OrderDetails } from "../models/order-details";
import { OrderUpdate } from "../models/order-update";
import { PaymentMethod } from "../models/payment-method";

@Injectable({
  providedIn: "root",
})
export class OrderService {
  private httpoption = {};
  constructor(private httpclient: HttpClient) {
    this.httpoption = {
      headers: new HttpHeaders({ "Content-Type": "application/json" }),
    };
  }

  GetAllorders(): Observable<Order[]> {
    return this.httpclient.get<Order[]>("http://localhost:3000/Orders");
  }

  GetorderById(oid: number): Observable<Order> {
    return this.httpclient.get<Order>(`http://localhost:3000/Orders?id=${oid}`);
  }

  updateorder(order: Order): Observable<Order> {
    return this.httpclient.put<Order>(
      `http://localhost:3000/Orders/${order.id}`,
      JSON.stringify(order),
      this.httpoption
    );
  }

  PlaceOrder(payment: PaymentMethod, addressId: string) {
    return this.httpclient.post(
      `${environment.api.baseURL}/api/Order/Add?PaymentMethod=${payment}&addressId=${addressId}`,
      addressId
    );
  }

  GetAll(): Observable<OrderUpdate[]> {
    return this.httpclient.get<OrderUpdate[]>(`${environment.api.baseURL}/api/Order/GetAll`);
  }

  OrderDetails(id: string): Observable<OrderDetails> {
    return this.httpclient.get<OrderDetails>(`${environment.api.baseURL}/api/Order/OrderDetails/?id=${id}`);
  }

  GetOrderDetails(id: string): Observable<Order> {
    return this.httpclient.get<Order>(`${environment.api.baseURL}/api/Order/GetOrderDetails/?id=${id}`);
  }
}
