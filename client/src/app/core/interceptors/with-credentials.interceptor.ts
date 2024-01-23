import { HttpEvent, HttpRequest, HttpHandlerFn, HttpInterceptorFn } from "@angular/common/http";
import { Observable } from "rxjs";

export const withCredentialsInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  // Add the withCredentials option to the request
  const modifiedRequest = req.clone({ withCredentials: true });
  return next(modifiedRequest);
};
