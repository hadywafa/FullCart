import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { NgIf } from "@angular/common";
import { AuthService } from "../../../shared/services/auth.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog } from "@angular/material/dialog";
import { SignUpComponent } from "../sign-up/sign-up.component";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.scss"],
  imports: [ReactiveFormsModule, NgIf],
  standalone: true,
})
export class SignInComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}
  ngOnDestroy(): void {
    // console.log("destroyed");
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ["", Validators.required],
      password: ["", Validators.required],
    });
  }

  get email() {
    return this.loginForm.get("email");
  }
  get password() {
    return this.loginForm.get("password");
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    this.auth.login({ email: this.email?.value ?? "", password: this.password?.value ?? "" }).subscribe({
      next: (data) => {
        this.snackBar.open("login  Successfully", "Success", {
          duration: 2000,
        });
        this.dialog.closeAll();
        // location.reload();
        // this.router.navigate(["/"]);
      },
      error: (error) => {
        this.snackBar.open("login is invalid due to wrong credentials or your account is not activated yet", "Failed", {
          duration: 2000,
        });
        this.dialog.closeAll();
      },
      complete: () => {},
    });
  }
  openSignUpDialog() {
    this.dialog.closeAll();
    this.dialog.open(SignUpComponent);
  }
}
