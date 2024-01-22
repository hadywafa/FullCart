import { Subscription } from "rxjs";
import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Product } from "../../models/product";
import { CartService } from "../../services/cart.service";
import { ProductsService } from "../../services/products.service";
import { NgIf } from "@angular/common";
import { LoaderComponent } from "../../../core/components/loader/loader.component";

@Component({
  selector: "app-product-details",
  templateUrl: "./product-details.component.html",
  styleUrls: ["./product-details.component.scss"],
  imports: [NgIf, LoaderComponent],
  standalone: true,
})
export class ProductDetailsComponent implements OnInit {
  product!: Product;
  productId!: number;
  isLoading = false;
  apiErrorMsg = "";
  constructor(
    private _activatedRoute: ActivatedRoute,
    private _productsService: ProductsService,
    private _cartService: CartService
  ) {}

  ngOnInit() {
    this.apiErrorMsg = "";
    this._activatedRoute.paramMap.subscribe((params) => {
      this.productId = +(params.get("id") ?? 0);
      this.isLoading = true;
      this._productsService.GetProductById(this.productId).subscribe({
        next: (res: Product) => {
          console.log(res);

          this.product = res;
          this.isLoading = false;
        },
        error: (err) => {
          this.apiErrorMsg = err.error.errors.msg;
          this.isLoading = false;
        },
      });
    });
  }

  addToCart(product: Product) {
    this._cartService.addToCart(product.id, 1);
  }
}
