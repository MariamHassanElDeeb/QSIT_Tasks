import { HTTP_INTERCEPTORS, HttpErrorResponse, HttpEvent, HttpEventType, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {


    const idToken = localStorage.getItem("id_token");

    if (idToken) {

      const cloned = req.clone({
        headers: req.headers.set("Authorization",
          "Bearer " + idToken)
      });


      return catch401(cloned, next, this.router);
    }
    else {
      return catch401(req, next, this.router);
    }

    function catch401(req: HttpRequest<any>, next: HttpHandler, router: Router) {
      return next.handle(req).pipe(
        catchError((err: HttpErrorResponse, observable) => {
          if (err.status === 401) {
            localStorage.removeItem("id_token")
            router.navigateByUrl('/login')
          }

          return throwError(() => err);
        })
      );
    }
  }
}


export const AuthInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
];
