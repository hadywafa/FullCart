import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { AuthService } from "../../../shared/services/auth.service";
import { SignUp } from "../../../shared/models/sign-up";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.scss"],
  imports:[ReactiveFormsModule],
  standalone: true,
})
export class SignUpComponent implements OnInit {
  signUpUser!: SignUp;
  userForm!: FormGroup;
  @Output() emmiterSignIn: EventEmitter<any> = new EventEmitter<any>();

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toaster: ToastrService
  ) {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      firstName: ["", [Validators.required, Validators.minLength(3)]],
      lastName: ["", [Validators.required, Validators.minLength(3)]],
    });
  }

  ngOnInit(): void {}

  get email() {
    return this.userForm.get("email");
  }
  get password() {
    return this.userForm.get("password");
  }
  get firstName() {
    return this.userForm.get("firstName");
  }
  get lastName() {
    return this.userForm.get("lastName");
  }

  onSubmit() {
    // stop here if form is invalid
    if (this.userForm.invalid) {
      return;
    }
    this.signUpUser = this.userForm.value;
    this.signUpUser.UserName = this.signUpUser.firstName + " " + this.signUpUser.lastName;
    this.signUpUser.role = "Customer";
    // console.log(this.vmSignUpUser);
    this.authService.register(this.signUpUser).subscribe(
      () => {
        this.toaster.success("Creating Accounting Successfully", "ok to start Shopping.");
        location.reload();
        this.router.navigate(["/"]);
      },
      (err: any) => {
        this.toaster.error("some error check your information and try again");
        console.warn(err);
      },
      () => {
        console.log("completed");
      }
    );
    this.userForm.reset();
  }
  goSignin() {
    this.emmiterSignIn.emit();
  }
}
