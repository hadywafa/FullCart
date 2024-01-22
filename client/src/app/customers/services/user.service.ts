import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "../../../environments/environment";
import { UserAddress } from "../models/user-address";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private httpoption = {};
  constructor(private httpclient: HttpClient) {
    this.httpoption = {
      headers: new HttpHeaders({ "Content-Type": "application/json" }),
    };
  }

  GetAllAddress(): Observable<UserAddress[]> {
    return this.httpclient.get<UserAddress[]>(`${environment.api.baseURL}/api/User/Addresses`);
  }

  addAddress(address: UserAddress): Observable<UserAddress> {
    return this.httpclient.post<UserAddress>(
      `${environment.api.baseURL}/api/User/AddAddress`,
      address,
      this.httpoption
    );
  }

  UpdateAddress(address: UserAddress): Observable<UserAddress> {
    return this.httpclient.put<UserAddress>(`${environment.api.baseURL}/api/User/UpdateAddress`, address);
  }

  deleteAddress(addressId: number) {
    return this.httpclient.delete<UserAddress>(
      `${environment.api.baseURL}/api/User/DeleteAddress?addressId=${addressId}`
    );
  }
  //Password Methods
  updatePassword(old_Password: string, new_Password: string) {
    let passObj = { oldPassword: old_Password, newPassword: new_Password };
    return this.httpclient.put(`${environment.api.baseURL}/api/User/UpdatePassword`, passObj);
  }
  //General Methods
  updateGeneralInfo(first: string, last: string) {
    return this.httpclient.put(`${environment.api.baseURL}/api/User/UpdateUserName?first=${first}&last=${last}`, null);
  }
}
