import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { SignUp } from "../models/sign-up";
import { environment } from "../../../environments/environment";
import { SignIn } from "../models/sign-in";
import { CookieService } from "ngx-cookie-service";
import User from "../../login/models/user";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.getIsAuthenticated());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  private currentUserSubject = new BehaviorSubject<User>(this.getCurrentUserFromCookie());
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private _api: HttpClient, private cookieService: CookieService) {}

  login(model: SignIn): Observable<any> {
    return this._api.post(
      `${environment.api.baseURL}` + `/Auth/Login?email=${model.email}&password=${model.password}`,
      model
    );
  }
  logout(): Observable<any> {
    return this._api.post(`${environment.api.baseURL}` + `/Auth/Logout`, null);
  }
  register(model: SignUp): Observable<any> {
    return this._api.post(`${environment.api.baseURL}` + "/Auth/Register", model);
  }
  //-----------------------------------------------------------
  updateAuthenticationStatus() {
    const isAuthenticated = this.getIsAuthenticated();
    this.isAuthenticatedSubject.next(isAuthenticated);

    const currentUser = this.getCurrentUserFromCookie();
    this.currentUserSubject.next(currentUser);
  }
  private getIsAuthenticated(): boolean {
    return this.cookieService.get("_id_token") ? true : false;
  }
  private getCurrentUserFromCookie(): User {
    const idToken = this.cookieService.get("_id_token");
    if (!idToken) {
      return {} as User;
    } else {
      return JSON.parse(idToken) as User;
    }
  }
}
