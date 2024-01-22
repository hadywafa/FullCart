import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: "app-customer-layout",
  templateUrl: "./customer-layout.component.html",
  styleUrls: ["./customer-layout.component.scss"],
  imports: [RouterOutlet],
  standalone: true,
})
export class CustomerLayoutComponent {}
