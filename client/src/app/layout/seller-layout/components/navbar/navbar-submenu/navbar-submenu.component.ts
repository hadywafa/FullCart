import { Component, Input, OnInit } from "@angular/core";
import { RouterLinkActive, RouterLink } from "@angular/router";
import { NgFor, NgTemplateOutlet, NgIf } from "@angular/common";
import { SubMenuItem } from "../../../../../core/models/menu.model";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: "div[navbar-submenu]",
  templateUrl: "./navbar-submenu.component.html",
  styleUrls: ["./navbar-submenu.component.scss"],
  standalone: true,
  imports: [NgFor, NgTemplateOutlet, RouterLinkActive, RouterLink, NgIf, MatIconModule],
})
export class NavbarSubmenuComponent implements OnInit {
  @Input() public submenu = <SubMenuItem[]>{};

  constructor() {}

  ngOnInit(): void {}
}
