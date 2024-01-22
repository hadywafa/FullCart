import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Category } from "../models/category";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  constructor(private _api: HttpClient) {}

  GetAllCategories(): Observable<Category[]> {
    return this._api.get<Category[]>(`http://localhost:3000/Categories`);
  }
  getCatParentsByCatId(): Observable<Category[]> {
    return this._api.get<Category[]>(`http://localhost:3000/Categories`);
  }
}
