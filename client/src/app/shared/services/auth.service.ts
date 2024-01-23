import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SignUp } from "../models/sign-up";
import { environment } from "../../../environments/environment";
import { SignIn } from "../models/sign-in";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private _api: HttpClient) {}

  login(model: SignIn): Observable<any> {
    console.log(model);

    return this._api.post(
      `${environment.api.baseURL}` + `/Auth/Login?email=${model.email}&password=${model.password}`,
      model,
      { withCredentials: true }
    );
  }

  register(model: SignUp): Observable<any> {
    return this._api.post(`${environment.api.baseURL}` + "/Auth/Register", model);
  }
}
