import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from "@angular/forms";
import { ProductsService } from "../../../../customers/services/products.service";
import { MatFormFieldModule } from "@angular/material/form-field";

@Component({
  selector: "app-add-inventory",
  templateUrl: "./add-inventory.component.html",
  styleUrls: ["./add-inventory.component.css"],
  imports: [ReactiveFormsModule, MatFormFieldModule],
  standalone: true,
})
export class AddInventoryComponent implements OnInit {
  productForm!: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductsService) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ["", Validators.required],
      description: ["", Validators.required],
      price: [null, [Validators.required, Validators.min(0)]],
      discount: [0, [Validators.min(0), Validators.max(100)]],
      image: [null, Validators.required],
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const formData = new FormData();
      formData.append("name", this.productForm.get("name")?.value);
      formData.append("description", this.productForm.get("description")?.value);
      formData.append("price", this.productForm.get("price")?.value);
      formData.append("discount", this.productForm.get("discount")?.value);

      const imageFile = this.productForm.get("image")?.value;
      formData.append("image", imageFile, imageFile.name);

      this.productService.createProduct(formData).subscribe(
        (response) => {
          console.log("Product created successfully:", response);
          // Add any additional logic or redirection
        },
        (error) => {
          console.error("Error creating product:", error);
          // Handle error
        }
      );
    }
  }

  onImageChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.productForm.patchValue({
        image: inputElement.files[0],
      });
    }
  }
}
