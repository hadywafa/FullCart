import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ThemeService } from "./core/utils/theme.service";
import { CommonModule, NgClass } from "@angular/common";
import { ResponsiveHelperComponent } from "./shared/components/responsive-helper/responsive-helper.component";
import { HttpClientModule } from "@angular/common/http";
import { IconService } from "./core/utils/icon.service";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [CommonModule, HttpClientModule, HttpClientModule, NgClass, RouterOutlet, ResponsiveHelperComponent],
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.scss",
})
export class AppComponent {
  title = "full-cart";

  constructor(public themeService: ThemeService, private iconService: IconService) {
    this.iconService.registerAppIcons();
  }
}
