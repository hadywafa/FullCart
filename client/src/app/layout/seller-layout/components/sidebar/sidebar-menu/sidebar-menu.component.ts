import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { MenuService } from "../../../services/menu.service";
import { SidebarSubmenuComponent } from "../sidebar-submenu/sidebar-submenu.component";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { NgFor, NgClass, NgTemplateOutlet, NgIf } from "@angular/common";
import { SubMenuItem } from "../../../../../core/models/menu.model";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: "app-sidebar-menu",
  templateUrl: "./sidebar-menu.component.html",
  styleUrls: ["./sidebar-menu.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    NgFor,
    NgClass,
    NgTemplateOutlet,
    RouterLink,
    RouterLinkActive,
    NgIf,
    SidebarSubmenuComponent,
    MatIconModule,
  ],
})
export class SidebarMenuComponent implements OnInit {
  constructor(public menuService: MenuService) {}

  public toggleMenu(subMenu: SubMenuItem) {
    this.menuService.toggleMenu(subMenu);
  }

  ngOnInit(): void {}
}
