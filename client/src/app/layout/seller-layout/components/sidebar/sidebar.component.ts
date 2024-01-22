import { Component, OnInit } from "@angular/core";
import { MenuService } from "../../services/menu.service";
import { RouterLink } from "@angular/router";
import { SidebarMenuComponent } from "./sidebar-menu/sidebar-menu.component";
import { NgClass, NgIf } from "@angular/common";
import { ThemeService } from "../../../../core/utils/theme.service";
import { MatIconModule } from "@angular/material/icon";
@Component({
  selector: "app-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"],
  standalone: true,
  imports: [NgClass, NgIf, SidebarMenuComponent, RouterLink, MatIconModule],
})
export class SidebarComponent implements OnInit {
  constructor(public themeService: ThemeService, public menuService: MenuService) {}

  ngOnInit(): void {}

  public toggleSidebar() {
    this.menuService.toggleSidebar();
  }

  toggleTheme() {
    this.themeService.theme = !this.themeService.isDark ? "dark" : "light";
  }
}
