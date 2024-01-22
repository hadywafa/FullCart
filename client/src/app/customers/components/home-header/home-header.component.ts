import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { Router, RouterModule } from "@angular/router";
import { environment } from "../../../../environments/environment";
import { TokenService } from "../../../login/services/token.service";
import { CartService } from "../../services/cart.service";
import { Category } from "../../models/category";
import { SignInComponent } from "../../../login/components/sign-in/sign-in.component";
import { GlobalsService } from "../../../shared/services/globals.service";
import { MatMenuModule } from "@angular/material/menu";
@Component({
  selector: "app-home-header",
  templateUrl: "./home-header.component.html",
  styleUrls: ["./home-header.component.scss"],
  imports: [MatMenuModule, RouterModule],
  standalone: true,
})
export class HomeHeaderComponent implements OnInit {
  apiUrl: string = environment.api.baseURL;
  localstorge: string;
  Categories!: Category[];
  token!: any;
  userName!: string;

  constructor(
    private router: Router,
    private _dialog: MatDialog,
    private global: GlobalsService,
    private _auth: TokenService,
    private _cartService: CartService // private _cartService: CartService
  ) {
    this.localstorge = localStorage.getItem("lang")!;
  }

  countCart!: number;
  ngOnInit(): void {
    //get statue from Cookie
    // if (localStorage.getItem("user")) {
    //   this._cartService.getCartItems().subscribe((prod) => (this.countCart = prod.length));
    // }
  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  register() {
    const dialogConfig = new MatDialogConfig();
    // dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    this._dialog.open(SignInComponent, dialogConfig);
  }
  logout() {
    document.cookie = "_statueId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    this.global.redirectToLogin();
    this.router.navigate(["/"]);
    window.location.reload();
  }
  searchtext: string = "";
  @Output()
  SearchTextCganged: EventEmitter<string> = new EventEmitter<string>();

  onSearchTextChange(st: string) {
    this.searchtext = st;
    this.SearchTextCganged.emit(this.searchtext);
  }

  loclaztion(st: string) {
    localStorage.setItem("lang", st);
    if (localStorage.getItem("ar") == "ar") {
      document.body.style.direction = "rtl";
    }
    location.reload();
  }
}
