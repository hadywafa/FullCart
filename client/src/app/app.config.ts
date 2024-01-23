import { ApplicationConfig } from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { provideAnimations } from "@angular/platform-browser/animations";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { provideToastr } from "ngx-toastr";
import { withCredentialsInterceptor } from "./core/interceptors/with-credentials.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideToastr(),
    provideHttpClient(),
    provideHttpClient(withInterceptors([withCredentialsInterceptor])),
  ],
};
