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
import { MatBadgeModule } from "@angular/material/badge";
import { APP_LANG } from "../../../core/models/app-lang";
import { AsyncPipe, NgIf } from "@angular/common";
import { SearchService } from "../../services/search-state.service";
import { AuthService } from "../../../shared/services/auth.service";
import User from "../../../login/models/user";
import { Observable } from "rxjs";

@Component({
  selector: "app-home-header",
  templateUrl: "./home-header.component.html",
  styleUrls: ["./home-header.component.scss"],
  imports: [MatMenuModule, MatBadgeModule, AsyncPipe, RouterModule, MatIconModule, NgIf],
  standalone: true,
})
export class HomeHeaderComponent {
  apiUrl: string = environment.api.baseURL;
  lang: string;
  Categories!: Category[];
  isAuthenticated!: boolean;
  currentUser!: User;
  userName!: string;
  cartItems$: Observable<number>;
  searchText: string = "";

  constructor(
    private search: SearchService,
    public global: GlobalsService,
    private dialog: MatDialog,
    private auth: AuthService,
    private cartService: CartService
  ) {
    this.auth.currentUser$.subscribe((currentUser) => {
      this.currentUser = currentUser;
    });
    this.auth.isAuthenticated$.subscribe((isAuthenticated) => {
      this.isAuthenticated = isAuthenticated;
    });

    this.lang = this.global.lang;

    this.cartItems$ = this.cartService.cartItems$;
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

  goToHome() {
    this.global.redirectToHome();
  }
  openCart() {
    this.global.redirectToComponent("cart");
  }
}
