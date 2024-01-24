import { ApplicationConfig, importProvidersFrom } from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { provideAnimations } from "@angular/platform-browser/animations";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { provideToastr } from "ngx-toastr";
import { withCredentialsInterceptor } from "./core/interceptors/with-credentials.interceptor";
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { WebpackTranslateLoader } from "./core/utils/webpack-translator-loader";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideToastr(),
    provideHttpClient(),
    provideHttpClient(withInterceptors([withCredentialsInterceptor])),
    importProvidersFrom(
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useClass: WebpackTranslateLoader,
        },
        isolate: false,
      })
    ),
  ],
};
