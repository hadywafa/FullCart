import { ActivatedRouteSnapshot, Routes } from "@angular/router";
import { CustomerLayoutComponent } from "./layout/customer-layout/customer-layout.component";
import { SellerLayoutComponent } from "./layout/seller-layout/seller-layout.component";
import { APP_TYPE_MODE } from "./core/models/app-type-mode";
import { NotFoundComponent } from "./core/components/not-found/not-found.component";
import { AppInitializationGuard } from "./core/guards/app-initialization.guard";

export const routes: Routes = [
  {
    path: `:appMode/:lang`,
    canActivate: [AppInitializationGuard],
    loadChildren: () => {
      const appMode = localStorage.getItem("appMode");
      if (appMode === APP_TYPE_MODE.SELLER) {
        return import("../app/seller/seller.routes").then((m) => m.SELLER_ROUTES);
      } else {
        return import("../app/customers/customer.routes").then((m) => m.CUSTOMER_ROUTES);
      }
    },
  },
  {
    path: "",
    redirectTo: `${APP_TYPE_MODE.SELLER}/en`,
    pathMatch: "full",
  },
  { path: "**", component: NotFoundComponent },
];
