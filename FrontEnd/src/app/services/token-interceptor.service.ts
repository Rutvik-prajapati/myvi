import { HttpErrorResponse, HttpInterceptor } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private injector : Injector,private router:Router) { }

  intercept(req : any, next : any){
    let authService = this.injector.get(AuthenticationService);
    let tokenizeReq = req.clone({
      setHeaders : {
        Authorization : `Bearer ${authService.getToken()}`
      }
    });
    return next.handle(tokenizeReq).pipe( tap(() => {},
    (err: any) => {
    if (err instanceof HttpErrorResponse) {
      if (err.status !== 401) {
       return;
      }
      localStorage.clear();
      this.router.navigate(['login']);
    }
    }));
  }
}
