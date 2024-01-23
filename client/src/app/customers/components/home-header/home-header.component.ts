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
  token!: any;
  userName!: string;
  countCart!: number;
  searchText: string = "";

  constructor(
    private router: Router,
    private search: SearchService,
    private global: GlobalsService,
    private dialog: MatDialog,
    private cartService: CartService // private _cartService: CartService
  ) {
    this.lang = localStorage.getItem("lang")!;
  }

  register() {
    this.dialog.closeAll();
    this.dialog.open(SignInComponent);
  }

  logout() {
    document.cookie = "_statueId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    this.global.redirectToLogin();
    this.router.navigate(["/"]);
    window.location.reload();
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
