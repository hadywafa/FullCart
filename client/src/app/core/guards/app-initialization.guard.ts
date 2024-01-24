import { TranslateService } from "@ngx-translate/core";
import { Injectable } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { Router, ActivatedRouteSnapshot, UrlTree } from "@angular/router";
import { Observable, of } from "rxjs";
import { GlobalsService } from "../../shared/services/globals.service";
import { APP_TYPE_MODE } from "../models/app-type-mode";
import { APP_LANG } from "../models/app-lang";

@Injectable({
  providedIn: "root",
})
export class AppInitializationGuard {
  constructor(
    private router: Router,
    private translateService: TranslateService,
    private global: GlobalsService,
    private titleService: Title
  ) {
    // this.translateService.setDefaultLang(APP_LANG.EN);
  }

  canActivate(next: ActivatedRouteSnapshot): Observable<boolean | UrlTree> | UrlTree {
    const appMode =
      next.paramMap.get("appMode") === APP_TYPE_MODE.SELLER ? APP_TYPE_MODE.SELLER : APP_TYPE_MODE.CUSTOMER;
    const lang = next.paramMap.get("lang") === APP_LANG.AR ? APP_LANG.AR : APP_LANG.EN;

    if (!appMode || !lang) {
      this.router.navigate([`page-not-found`]).catch((e) => console.log(e));
      return this.router.createUrlTree(["/"]);
    }
    localStorage.setItem("appMode", appMode);
    this.global.Initialize(appMode, lang);

    // this.translateService.use(lang);

    this.titleService.setTitle(this.global.appMode);

    return of(true);
  }
}
