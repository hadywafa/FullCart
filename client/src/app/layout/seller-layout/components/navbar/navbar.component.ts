import { Component, OnInit } from "@angular/core";
import { MenuService } from "../../services/menu.service";
import { NavbarMobileComponent } from "./navbar-mobile/navbar-mobile.component";
import { ProfileMenuComponent } from "./profile-menu/profile-menu.component";
import { NavbarMenuComponent } from "./navbar-menu/navbar-menu.component";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"],
  standalone: true,
  imports: [MatIconModule, NavbarMenuComponent, ProfileMenuComponent, NavbarMobileComponent],
})
export class NavbarComponent implements OnInit {
  constructor(private menuService: MenuService) {}

  ngOnInit(): void {}

  public toggleMobileMenu(): void {
    this.menuService.showMobileMenu = true;
  }
}
