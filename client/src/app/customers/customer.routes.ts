import { Routes } from "@angular/router";
import { NotFoundComponent } from "../core/components/not-found/not-found.component";
import { CartComponent } from "./components/cart/cart.component";
import { CheckoutComponent } from "./components/checkout/checkout.component";
import { AccountComponent } from "./pages/account/account.component";
import { ProductsComponent } from "./pages/products/products.component";
import { OrdersComponent } from "./pages/orders/orders.component";

export const CUSTOMER_ROUTES: Routes = [
  { path: "", component: ProductsComponent },
  { path: "account", component: AccountComponent },
  { path: "cart", component: CartComponent },
  { path: "orders", component: OrdersComponent },
  { path: "checkout", component: CheckoutComponent },
  { path: "**", component: NotFoundComponent },
];
