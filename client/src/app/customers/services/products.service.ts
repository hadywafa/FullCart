import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Category } from "../models/category";
import { Product } from "../models/product";
import { ProductSummary } from "../models/product-bref";
import { Review } from "../models/review";

@Injectable({
  providedIn: "root",
})
export class ProductsService {
  constructor(private httpClient: HttpClient) {}

  GetAllProducts(): Observable<ProductSummary[]> {
    return this.httpClient.get<ProductSummary[]>(`${environment.api.baseURL}` + "/Products");
  }

  GetProductsByCatCode(catCode: string): Observable<ProductSummary[]> {
    return this.httpClient.get<ProductSummary[]>(
      `${environment.api.baseURL}` + `/Products/FilterProductsByCatCode/${catCode}`
    );
  }

  GetProductById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(`${environment.api.baseURL}` + `/Products/${id}`);
  }

  GetAllProductReviews(proId: number): Observable<Review[]> {
    return this.httpClient.get<Review[]>(`${environment.api.baseURL}` + `/Products/GetProductReviews/${proId}`);
  }

  GetCategoriesJson(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${environment.api.baseURL}` + "/Products/GetCategoriesJson");
  }

  GetProductCategories(catId: number): Observable<Category[]> {
    return this.httpClient.get<Category[]>(
      `${environment.api.baseURL}` + `/Products/GetProductCategories?parentCatId=${catId}`
    );
  }
}
