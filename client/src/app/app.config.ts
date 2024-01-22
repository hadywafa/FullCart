import { ApplicationConfig } from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { provideAnimations } from "@angular/platform-browser/animations";
import { provideHttpClient } from "@angular/common/http";
import { DataService } from "./core/data/data.service";
import { HttpClientInMemoryWebApiModule } from "angular-in-memory-web-api";
import { WrapperModule } from "./core/modules/wrapper.module";
import { provideToastr } from "ngx-toastr";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimations(), provideToastr(), provideHttpClient(), WrapperModule],
};
