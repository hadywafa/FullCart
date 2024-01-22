import { Routes } from "@angular/router";
import { CustomerLayoutComponent } from "./layout/customer-layout/customer-layout.component";
import { SellerLayoutComponent } from "./layout/seller-layout/seller-layout.component";
import { APP_TYPE_MODE } from "./core/models/app-type-mode";
import { NotFoundComponent } from "./core/components/not-found/not-found.component";

export const routes: Routes = [
  {
    path: `${APP_TYPE_MODE.CUSTOMER}/:lang`,
    component: CustomerLayoutComponent,
    loadChildren: () => import("../app/customers/customer.routes").then((m) => m.CUSTOMER_ROUTES),
  },
  {
    path: `${APP_TYPE_MODE.SELLER}/:lang`,
    component: SellerLayoutComponent,
    loadChildren: () => import("../app/seller/seller.routes").then((m) => m.SELLER_ROUTES),
  },
  {
    path: "",
    redirectTo: `${APP_TYPE_MODE.SELLER}/en`,
    pathMatch: "full",
  },
  { path: "**", component: NotFoundComponent },
];
