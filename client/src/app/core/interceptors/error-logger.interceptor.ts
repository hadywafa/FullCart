import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { throwError } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { inject } from "@angular/core";

export const errorLoggingInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const toastersService = inject(ToastrService);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.warn(error);
      let errorMsg = "";
      if (error.error instanceof ErrorEvent) {
        // Client-side error
        errorMsg = `Client Error: ${error.error.message}`;
      } else {
        // Server-side error
        errorMsg = `Server Error: ${error.status} ${error.statusText}`;
      }
      // Display the error message using Toastr
      toastersService.error(errorMsg, "Error");

      return throwError(() => new Error(errorMsg));
    })
  );
};
