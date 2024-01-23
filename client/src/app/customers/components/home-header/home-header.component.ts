import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router, RouterModule } from "@angular/router";
import { environment } from "../../../../environments/environment";
import { CartService } from "../../services/cart.service";
import { Category } from "../../models/category";
import { SignInComponent } from "../../../login/components/sign-in/sign-in.component";
import { GlobalsService } from "../../../shared/services/globals.service";
import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { APP_LANG } from "../../../core/models/app-lang";
import { NgIf } from "@angular/common";
import { SearchService } from "../../services/search-state.service";
import { AppLocalStorageService } from "../../../shared/services/app-local-storage.service";
import { AuthService } from "../../../shared/services/auth.service";
import User from "../../../login/models/user";
import { CookieService } from "ngx-cookie-service";

@Component({
  selector: "app-home-header",
  templateUrl: "./home-header.component.html",
  styleUrls: ["./home-header.component.scss"],
  imports: [MatMenuModule, RouterModule, MatIconModule, NgIf],
  standalone: true,
})
export class HomeHeaderComponent {
  apiUrl: string = environment.api.baseURL;
  lang: string;
  Categories!: Category[];
  isAuthenticated!: boolean;
  currentUser!: User;
  userName!: string;
  countCart!: number;
  searchText: string = "";

  constructor(
    private cookieService: CookieService,
    private search: SearchService,
    public global: GlobalsService,
    private dialog: MatDialog,
    private auth: AuthService
  ) {
    this.auth.currentUser$.subscribe((currentUser) => {
      this.currentUser = currentUser;
    });
    this.auth.isAuthenticated$.subscribe((isAuthenticated) => {
      this.isAuthenticated = isAuthenticated;
    });

    this.lang = this.global.lang;
  }

  register() {
    this.dialog.closeAll();
    this.dialog.open(SignInComponent);
  }

  logout() {
    this.auth.logout().subscribe(() => {
      this.auth.updateAuthenticationStatus();
      this.global.redirectToHome();
    });
  }

  onSearchTextChange(st: string) {
    this.searchText = st;
    this.search.emitSearchTextChanged(this.searchText);
  }

  localization(lang: string) {
    this.global.Initialize(this.global.appMode, lang as APP_LANG);
    location.reload();
  }
  goToCart() {
    this.global.redirectToComponent("cart");
  }
  goToHome() {
    this.global.redirectToHome();
  }
}
