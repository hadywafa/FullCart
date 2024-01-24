import { NgIf } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-update-inventory",
  templateUrl: "./update-inventory.component.html",
  styleUrls: ["./update-inventory.component.css"],
  imports: [ReactiveFormsModule, MatFormFieldModule, MatButtonModule, NgIf],
  standalone: true,
})
export class UpdateInventoryComponent implements OnInit {
  productForm!: FormGroup;
  productId: number;

  constructor(private fb: FormBuilder, private route: ActivatedRoute) {
    this.productId = +(this.route.snapshot.paramMap.get("id") ?? 0);
  }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ["", Validators.required],
      description: ["", Validators.required],
      buyingPrice: [null, Validators.required],
      sellingPrice: [null, Validators.required],
      discount: [null, Validators.required],
      quantity: [null, Validators.required],
      weight: ["", Validators.required],
      // Add other form controls based on your Product model
    });

    this.fetchProductDetails();
  }

  fetchProductDetails() {
    // Implement logic to fetch product details by productId and populate the form
    // Example:
    // this.productService.getProductDetails(this.productId).subscribe(product => {
    //   this.productForm.patchValue(product);
    // });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const formData = this.productForm.value;
      // Implement logic to update the product using formData
      // Example:
      // this.productService.updateProduct(this.productId, formData).subscribe(response => {
      //   // Handle success or error
      // });
    }
  }
}
