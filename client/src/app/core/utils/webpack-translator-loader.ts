import { TranslateLoader } from "@ngx-translate/core";
import { from, pluck } from "rxjs";

export class WebpackTranslateLoader implements TranslateLoader {
  getTranslation(lang: string) {
    return from(import(`src/assets/i18n/${lang}`)).pipe(pluck("default"));
  }
}
