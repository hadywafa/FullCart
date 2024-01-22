import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { HomeHeaderComponent } from "../../customers/components/home-header/home-header.component";

@Component({
  selector: "app-customer-layout",
  templateUrl: "./customer-layout.component.html",
  styleUrls: ["./customer-layout.component.scss"],
  imports: [RouterOutlet, HomeHeaderComponent],
  standalone: true,
})
export class CustomerLayoutComponent {}
