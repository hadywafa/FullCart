import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { TokenService } from "../../services/token.service";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.scss"],
  imports: [ReactiveFormsModule],
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
    private _fb: FormBuilder,
    private _router: Router,
    private tokenService: TokenService,
    private toaster: ToastrService
  ) {}
  ngOnDestroy(): void {
    // console.log("destroyed");
  }

  ngOnInit(): void {
    this.loginForm = this._fb.group({
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
    this.tokenService.requestAccessToken(this.email?.value ?? "", this.password?.value ?? "").subscribe({
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
        this._router.navigate(["/"]);
        window.location.reload();
      },
    });
  }
  goSignUp() {
    this.emmiterSignUp.emit();
  }
  //#endregion
}
