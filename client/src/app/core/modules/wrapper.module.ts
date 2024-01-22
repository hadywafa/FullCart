// wrapper.module.ts
import { NgModule } from "@angular/core";
import { HttpClientInMemoryWebApiModule } from "angular-in-memory-web-api";
import { DataService } from "../data/data.service";

@NgModule({
  imports: [
    HttpClientInMemoryWebApiModule.forRoot(DataService, {
      dataEncapsulation: false,
    }),
  ],
})
export class WrapperModule {}
