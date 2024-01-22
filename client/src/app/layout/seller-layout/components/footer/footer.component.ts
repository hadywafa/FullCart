import { Component, OnInit } from "@angular/core";
import { GlobalsService } from "../../../../shared/services/globals.service";
import { APP_TYPE_MODE } from "../../../../core/models/app-type-mode";

@Component({
  selector: "app-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.scss"],
  standalone: true,
})
export class FooterComponent implements OnInit {
  public year: number = new Date().getFullYear();

  constructor(private global: GlobalsService) {}
  redirectToCustomer() {
    this.global.Initialize(APP_TYPE_MODE.CUSTOMER, this.global.lang);
    this.global.redirectToHome();
  }
  ngOnInit(): void {}
}
