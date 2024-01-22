import { Component, OnInit } from "@angular/core";
import { ProductSummary } from "../../models/product-bref";
import { environment } from "../../../../environments/environment";
import { ProductsService } from "../../services/products.service";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import { HomeHeaderComponent } from "../../components/home-header/home-header.component";
import { CommonModule } from "@angular/common";
import { AfterDiscountPricePipe } from "../../../core/pipes/after-discount-price.pipe";
import { RouterModule } from "@angular/router";
import { StringLengthPipe } from "../../../core/pipes/string-length.pipe";
@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.scss"],
  imports: [
    MatProgressSpinner,
    HomeHeaderComponent,
    CommonModule,
    StringLengthPipe,
    AfterDiscountPricePipe,
    RouterModule,
  ],
  standalone: true,
})
export class ProductsComponent implements OnInit {
  Products!: ProductSummary[];
  page: number = 1;
  count: number = 0;
  isspener: boolean = true;
  productSize: number = 20;
  productSizes: any = [5, 10, 15, 20];
  localstorge: string = "en";
  arRegx: string = "/[\u0600-\u06FF]/";
  imagesBaseUrl: string = environment.api.storagePath;

  constructor(private productsService: ProductsService) {
    if (localStorage.getItem("lang")) this.localstorge = localStorage.getItem("lang")!;
  }

  ngOnInit(): void {
    this.productsService.GetAllProducts().subscribe((productlist) => {
      this.Products = productlist;
      if (this.Products != null) {
        this.isspener = false;
        document.getElementById("pop")!.style.display = "none";
      }
    });
  }

  onDataChange(event: any) {
    this.page = event;
  }

  onSizeChange(event: any) {
    this.productSize = event.target.value;
    this.page = 1;
  }
  SreachText: string = "";

  onSearchTextEnterd(searchvalue: string) {
    this.SreachText = searchvalue.toLowerCase();

    console.log(this.SreachText);

    if (this.SreachText !== "") {
      this.Products =
        this.Products.filter((p) => p.name?.toLowerCase().includes(this.SreachText)) ||
        this.Products.filter((p) => p.description?.toLowerCase().includes(this.SreachText));

      console.log(this.Products);
    } else {
      this.isspener = true;
      document.getElementById("pop")!.style.display = "block";
      this.productsService.GetAllProducts().subscribe((productlist) => {
        this.Products = productlist;
        if (this.Products != null) {
          this.isspener = false;
          document.getElementById("pop")!.style.display = "none";
        }
      });
    }
  }
}
