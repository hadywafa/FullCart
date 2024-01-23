import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { CartProduct } from "../models/cart-product";

@Injectable({
  providedIn: "root",
})
export class CartService {
  private cartItemsSubject = new BehaviorSubject<number>(0);
  cartItems$ = this.cartItemsSubject.asObservable();

  constructor(private _api: HttpClient) {}

  //get all cart items
  getCartItems(): Observable<CartProduct[]> {
    return this._api.get<CartProduct[]>(`${environment.api.baseURL}` + `/Cart`);
  }

  // Add to cart
  addToCart(proId: number, count: number) {
    return this._api.post(`${environment.api.baseURL}` + `/Cart/Add?proId=${proId}&count=${count}`, count);
  }
  //update product count in cart
  updateQuantity(proId: number, count: number) {
    return this._api.post(`${environment.api.baseURL}` + `/Cart/Update?proId=${proId}`, count);
  }
  //Remove from cart
  removeFromCart(proId: number) {
    return this._api.delete(`${environment.api.baseURL}` + `/Cart/Remove?proId=${proId}`);
  }

  GetTotalPrice(): Observable<number> {
    return this._api.get<number>(`${environment.api.baseURL}/Cart/GetCartPrice`);
  }
}
