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
import { GlobalsService } from "../../../shared/services/globals.service";
import { SearchService } from "../../services/search-state.service";
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
  isLoading: boolean = true;
  productSize: number = 20;
  productSizes: any = [5, 10, 15, 20];
  lang: string = "en";
  arabicRegex: string = "/[\u0600-\u06FF]/";
  imagesBaseUrl: string = environment.api.storagePath;

  constructor(
    private searchService: SearchService,
    private productsService: ProductsService,
    private global: GlobalsService
  ) {
    this.global.lang;
  }

  ngOnInit(): void {
    this.productsService.GetAllProducts().subscribe((products) => {
      this.Products = products;
      if (this.Products != null) {
        this.isLoading = false;
        document.getElementById("pop")!.style.display = "none";
      }
    });

    this.searchService.searchTextChanged$.subscribe((searchText) => {
      this.onSearchTextEntered(searchText);
    });
  }

  onDataChange(event: any) {
    this.page = event;
  }

  onSizeChange(event: any) {
    this.productSize = event.target.value;
    this.page = 1;
  }
  SearchText: string = "";

  onSearchTextEntered(searchValue: string) {
    this.SearchText = searchValue.toLowerCase();

    if (this.SearchText !== "") {
      this.Products =
        this.Products.filter((p) => p.name?.toLowerCase().includes(this.SearchText)) ||
        this.Products.filter((p) => p.description?.toLowerCase().includes(this.SearchText));

      console.log(this.Products);
    } else {
      this.isLoading = true;
      document.getElementById("pop")!.style.display = "block";
      this.productsService.GetAllProducts().subscribe((products) => {
        this.Products = products;
        if (this.Products != null) {
          this.isLoading = false;
          document.getElementById("pop")!.style.display = "none";
        }
      });
    }
  }
}
