import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, map, Observable } from "rxjs";
import { SignUp } from "../models/sign-up";
import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private _api: HttpClient) {}

  register(model: SignUp): Observable<any> {
    return this._api.post(`${environment.api.baseURL}` + "/api/Auth/Register", model);
  }
}
