import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { WishList } from "../models/wishlist";

@Injectable({
  providedIn: "root",
})
export class WishListService {
  constructor(private _api: HttpClient) {}

  getWishListItems(): Observable<WishList[]> {
    return this._api.get<WishList[]>(`${environment.api.baseURL}` + `/api/Wishlist/GetAll`);
  }

  // Add to cart
  addToWishList(proId: number) {
    return this._api.post(`${environment.api.baseURL}` + `/api/Wishlist/Add?proId=${proId}`, null);
  }

  //Remove from cart
  removeFromWishList(proId: number) {
    return this._api.delete(`${environment.api.baseURL}` + `/api/Wishlist/Remove?proId=${proId}`);
  }
}
