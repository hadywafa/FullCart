import { Injectable } from "@angular/core";
import { DomSanitizer, SafeResourceUrl } from "@angular/platform-browser";
import { MatIconRegistry } from "@angular/material/icon";
import { MenuService } from "../../layout/seller-layout/services/menu.service";

@Injectable({
  providedIn: "root",
})
export class IconService {
  constructor(
    private menuService: MenuService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {}

  // Method to register SVG icon from the asset folder
  private registerSvgIconFromAsset(iconName: string, path: string): void {
    const safePath: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl(path);
    this.iconRegistry.addSvgIcon(iconName, safePath);
  }
  registerAppIcons() {
    this.menuService.pagesMenu.forEach((group) => {
      group.items.forEach((x) => {
        if (x.iconName && x.iconPath) this.registerSvgIconFromAsset(x.iconName, x.iconPath);
      });
    });
    this.registerSvgIconFromAsset("app_arrow_left", "assets/icons/heroicons/solid/chevron-double-left.svg");
    this.registerSvgIconFromAsset("app_sign_out", "assets/icons/heroicons/outline/logout.svg");
    this.registerSvgIconFromAsset("app_theme_dark", "assets/icons/heroicons/outline/sun.svg");
    this.registerSvgIconFromAsset("app_theme_light", "assets/icons/heroicons/outline/moon.svg");
  }
}
