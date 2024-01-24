import { Subscription } from "rxjs";
import { Component, Input, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { Product } from "../../models/product";
import { CartService } from "../../services/cart.service";
import { MatIcon } from "@angular/material/icon";
@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["./product.component.scss"],
  imports: [MatIcon],
  standalone: true,
})
export class ProductComponent implements OnDestroy {
  @Input() product!: Product;
  constructor(private _router: Router, private cartService: CartService) {}
  sub!: Subscription;

  goToDetails(productId: number) {
    this._router.navigate(["/productDetails/", productId]);
  }

  addToCart(product: Product) {
    this.sub = this.cartService.addToCart(product.id, 1).subscribe();
  }
  ngOnDestroy(): void {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
