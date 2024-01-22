import { Injectable } from "@angular/core";
import { TokenService } from "../../login/services/token.service";
import User from "../../login/models/user";
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: "root",
})
export class AppLocalStorageService {
  constructor(private tokenService: TokenService) {}
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
    const accessToken = this.tokenService.getAccessToken();
    if (!accessToken) {
      // this.globalService.redirectToLogin();
      return {} as User;
    }

    try {
      const tokenData: any = jwtDecode(accessToken);
      const contactDataStr = tokenData.contact_data;
      const userData = {
        ...(contactDataStr ? JSON.parse(contactDataStr) : {}),
      } as User;

      return userData;
    } catch (error) {
      console.error("Error decoding token:", error);
      return {} as User;
    }
  }
  //--------- Cart -----------------//
  //--------- Wishlist -----------------//
}
