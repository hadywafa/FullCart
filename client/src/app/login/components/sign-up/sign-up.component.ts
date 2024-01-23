import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { AuthService } from "../../../shared/services/auth.service";
import { SignUp } from "../../../shared/models/sign-up";
import { SignInComponent } from "../sign-in/sign-in.component";
import { MatSnackBar } from "@angular/material/snack-bar";
@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.scss"],
  imports: [ReactiveFormsModule],
  standalone: true,
})
export class SignUpComponent implements OnInit {
  signUpUser!: SignUp;
  userForm!: FormGroup;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
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
    if (this.userForm.invalid) {
      this.snackBar.open("invalid form", "Failed", {
        duration: 2000,
      });
      return;
    }
    this.signUpUser = this.userForm.value;
    this.signUpUser.UserName = this.signUpUser.firstName + " " + this.signUpUser.lastName;
    this.signUpUser.role = "Customer";
    this.authService.register(this.signUpUser).subscribe(
      () => {
        this.snackBar.open("Creating Accounting Successfully", "Success", {
          duration: 2000,
        });
        this.dialog.closeAll();
        // location.reload();
        // this.router.navigate(["/"]);
      },
      (err: any) => {
        this.snackBar.open("some error check your information and try again", "Failed", {
          duration: 2000,
        });
        console.warn(err);
      }
    );
    this.userForm.reset();
  }
  openSignInDialog() {
    this.dialog.closeAll();
    this.dialog.open(SignInComponent);
  }
}
