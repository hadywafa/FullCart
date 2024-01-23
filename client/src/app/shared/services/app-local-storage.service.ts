import { Injectable } from "@angular/core";
import User from "../../login/models/user";
import { jwtDecode } from "jwt-decode";
import { CookieService } from "ngx-cookie-service";

@Injectable({
  providedIn: "root",
})
export class AppLocalStorageService {
  constructor(private cookieService: CookieService) {}
  //----------------------------------------------------------------------
  private getDataItem(key: string): any | null {
    const data = localStorage.getItem(key);
    return data ? JSON.parse(data) : null;
  }

  private setDataItem(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  private clearDataItem(key: string): void {
    localStorage.removeItem(key);
  }

  //---------LogIn User Info-----------------//
  getCurrentUser(): User {
    const id_token = this.cookieService.get("_id_token");
    if (!id_token) {
      // this.globalService.redirectToLogin();
      return {} as User;
    } else return JSON.parse(id_token) as User;
  }
  //--------- Cart -----------------//
  //--------- Wishlist -----------------//
}
