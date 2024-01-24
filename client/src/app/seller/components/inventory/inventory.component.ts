import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ProductSummary } from "../../../customers/models/product-bref";
import { ProductsService } from "../../../customers/services/products.service";
import { MatTableDataSource } from "@angular/material/table";
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";
import { GlobalsService } from "../../../shared/services/globals.service";
import { MatButtonModule } from "@angular/material/button";

@Component({
  selector: "app-inventory",
  templateUrl: "./inventory.component.html",
  styleUrls: ["./inventory.component.css"],
  imports: [MatTableModule, MatIconModule, MatButtonModule],
  standalone: true,
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ["name", "price", "quantity", "actions"];
  dataSource: MatTableDataSource<ProductSummary> = new MatTableDataSource<ProductSummary>();

  constructor(private global: GlobalsService, private productService: ProductsService, private router: Router) {}

  ngOnInit(): void {
    this.productService.GetAllProducts().subscribe((products) => {
      this.dataSource = new MatTableDataSource(products);
    });
  }

  updateProduct(productId: number): void {
    this.global.redirectToComponent(`inventory/update/${productId}`);
  }

  addProduct(): void {
    this.global.redirectToComponent(`inventory/add`);
  }
}
