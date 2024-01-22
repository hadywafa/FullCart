import { Subscription } from 'rxjs';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/Models/product';
import { CartService } from 'src/app/Services/cart/cart.service';
import { ProductsService } from 'src/app/Services/products/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product!: Product;
  productId!: string
  isLoading = false
  apiErrorMsg = ''
  constructor(private _activatedRoute: ActivatedRoute, private _productsService : ProductsService, private _cartService : CartService) {}

  ngOnInit() {
    this.apiErrorMsg = ''
    this._activatedRoute.paramMap.subscribe((params) => {
      this.productId = <string>params.get('id');
      this.isLoading = true
      this._productsService.getSpecificProduct(this.productId).subscribe({
        next: (res : Product)=>{
          this.product = res;
          this.isLoading = false
        },
        error: (err)=>{
          this.apiErrorMsg = err.error.errors.msg
          this.isLoading = false
        }
      });
    });
  }

  addToCart(product: Product) {
    this._cartService.setCartProducts(product)
  }
}

