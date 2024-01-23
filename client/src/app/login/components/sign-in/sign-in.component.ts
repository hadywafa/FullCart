import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { TokenService } from "../../services/token.service";
import { NgIf } from "@angular/common";
import { AuthService } from "../../../shared/services/auth.service";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.scss"],
  imports: [ReactiveFormsModule, NgIf],
  standalone: true,
})
export class SignInComponent implements OnInit {
  //=========================================================Properties====================================
  //#region Properties
  loginForm!: FormGroup;
  @Output() emmiterSignUp: EventEmitter<any> = new EventEmitter<any>();
  //#endregion
  //========================================================Lifecycle Hooks==================================
  //#region Lifecycle Hooks
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private auth: AuthService,
    private tokenService: TokenService,
    private toaster: ToastrService
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
  //#endregion
  //===========================================================Methods======================================
  //#region Methods
  // convenience getter for easy access to form fields
  get email() {
    return this.loginForm.get("email");
  }
  get password() {
    return this.loginForm.get("password");
  }

  onSubmit() {
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.auth.login({ email: this.email?.value ?? "", password: this.password?.value ?? "" }).subscribe({
      next: (data) => {
        localStorage.setItem("user", JSON.stringify(data));
      },
      error: (error) => {
        console.log(error);
        this.toaster.error(
          "login is invalid due to wrong credentials or your account is not activated yet",
          "try again later."
        );
      },
      complete: () => {
        this.toaster.success("login  Successfully", "compelet Shopping  ");
        // this.router.navigate(["/"]);
        // window.location.reload();
      },
    });
  }
  goSignUp() {
    this.emmiterSignUp.emit();
  }
  //#endregion
}
