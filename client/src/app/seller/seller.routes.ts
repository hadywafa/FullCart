import { Routes } from "@angular/router";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { NotFoundComponent } from "../core/components/not-found/not-found.component";
import { InventoryComponent } from "./components/inventory/inventory.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { AddInventoryComponent } from "./components/inventory/add-inventory/add-inventory.component";
import { UpdateBrandComponent } from "./components/brands/update-brand/update-brand.component";
import { CategoriesComponent } from "./components/categories/categories.component";
import { AddCategoryComponent } from "./components/categories/add-category/add-category.component";
import { UpdateCategoryComponent } from "./components/categories/update-category/update-category.component";
import { BrandsComponent } from "./components/brands/brands.component";
import { AddBrandComponent } from "./components/brands/add-brand/add-brand.component";
import { OrdersComponent } from "./components/orders/orders.component";

export const SELLER_ROUTES: Routes = [
  { path: "dashboard", component: DashboardComponent },
  { path: "inventory", component: InventoryComponent },
  {
    path: "inventory",
    children: [
      { path: "", component: InventoryComponent },
      { path: "add", component: AddInventoryComponent },
      { path: "update/:id", component: UpdateBrandComponent },
    ],
  },
  {
    path: "category",
    children: [
      { path: "", component: CategoriesComponent },
      { path: "add", component: AddCategoryComponent },
      { path: "update/:id", component: UpdateCategoryComponent },
    ],
  },
  {
    path: "brand",
    children: [
      { path: "", component: BrandsComponent },
      { path: "add", component: AddBrandComponent },
      { path: "update/:id", component: UpdateBrandComponent },
    ],
  },
  { path: "order", component: OrdersComponent },
  { path: "profile", component: ProfileComponent },
  { path: "", redirectTo: "dashboard", pathMatch: "full" },
  { path: "**", component: NotFoundComponent },
];
