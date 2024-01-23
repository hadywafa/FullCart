# Interceptors

## References

### <https://medium.com/@santosant/angular-functional-interceptors-3a2a2e71cdef>

### Steps

1. Define

```ts
import {
 HttpErrorResponse,
 HttpHandlerFn,
 HttpInterceptorFn,
 HttpRequest,
} from "@angular/common/http";
import { catchError, throwError } from "rxjs";

export const httpErrorInterceptor: HttpInterceptorFn = (
 req: HttpRequest<unknown>,
 next: HttpHandlerFn,
) => {
 return next(req).pipe(
  catchError((error: HttpErrorResponse) => {
   let errorMsg = "";
   if (error.error instanceof ErrorEvent) {
    console.log("this is client side error");
    errorMsg = `Client Error: ${error.error.message}`;
   } else {
    console.log("this is server side error");
    errorMsg = `Server Error Code: ${error.status}, Message: ${error.message}`;
   }

   console.log(errorMsg);
   return throwError(() => errorMsg);
  }),
 );
};
```

+ An interceptor function takes two parameters:
  + The request with which the interceptor will interact.
  + A function that allows you to send the transformed request.
+ The return value of this function should be an `Observable<HttpEvent<any>>`, of type HttpEvent, to effectively interact with the HTTP request’s response.

1. To add an interceptor function to the HttpClient instance.

+ new function called withInterceptors() is provided. This function takes an array of interceptor functions as its parameter and returns an HttpFeature of the interceptor type. This return type is highly useful as it allows you to subsequently invoke the function in the `provideHttpClient()` function.

```ts
import { httpErrorInterceptor } from "@/interceptors";

boostrapApplication(AppCmp, {
 providers: [provideHttpClient(withInterceptors([HttpErrorInterceptor]))],
}).catch(console.error);
```
