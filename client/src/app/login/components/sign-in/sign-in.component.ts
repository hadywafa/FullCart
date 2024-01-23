import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { NgIf } from "@angular/common";
import { AuthService } from "../../../shared/services/auth.service";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.scss"],
  imports: [ReactiveFormsModule, NgIf],
  standalone: true,
})
export class SignInComponent implements OnInit {
  loginForm!: FormGroup;
  @Output() emmiterSignUp: EventEmitter<any> = new EventEmitter<any>();

  constructor(private fb: FormBuilder, private auth: AuthService, private snackBar: MatSnackBar) {}
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
        localStorage.setItem("user", JSON.stringify(data));
      },
      error: (error) => {
        console.log(error);
        this.snackBar.open(
          "login is invalid due to wrong credentials or your account is not activated yet",
          "try again later.",
          {
            duration: 2000,
          }
        );
      },
      complete: () => {
        this.snackBar.open("login  Successfully", "Login", {
          duration: 2000,
        });
      },
    });
  }
  goSignUp() {
    this.emmiterSignUp.emit();
  }
}
