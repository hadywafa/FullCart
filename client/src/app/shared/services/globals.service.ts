import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { APP_TYPE_MODE } from "../../core/models/app-type-mode";
import { APP_LANG } from "../../core/models/app-lang";
@Injectable({
  providedIn: "root",
})
export class GlobalsService {
  appMode: APP_TYPE_MODE = APP_TYPE_MODE.CUSTOMER;
  lang: APP_LANG = APP_LANG.EN;

  constructor(private router: Router) {}

  Initialize(appMode: APP_TYPE_MODE, lang: APP_LANG): void {
    this.appMode = appMode;
    this.lang = lang;
  }

  redirectToLogin() {
    this.appMode === APP_TYPE_MODE.CUSTOMER
      ? this.router.navigate([`../${this.appMode}/${this.lang}/login`]).catch((e) => console.log(e))
      : this.router.navigate([`../${this.appMode}/${this.lang}/login`]).catch((e) => console.log(e));
  }

  redirectToHome() {
    this.appMode === APP_TYPE_MODE.CUSTOMER
      ? this.router.navigate([`../${this.appMode}/${this.lang}`]).catch((e) => console.log(e))
      : this.router.navigate([`../${this.appMode}/${this.lang}`]).catch((e) => console.log(e));
  }

  redirectToComponent(componentName: string) {
    this.appMode === APP_TYPE_MODE.CUSTOMER
      ? this.router
          .navigate([`../${this.appMode}/${this.lang}/${this.lang}/${componentName}`])
          .catch((e) => console.log(e))
      : this.router.navigate([`../${this.appMode}/${this.lang}/${componentName}`]).catch((e) => console.log(e));
  }
}
