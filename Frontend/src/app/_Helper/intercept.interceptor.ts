import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {AccountService} from '../account/Account.service'

const TOKEN_HEADER_KEY = 'Authorization';       // for back-end

@Injectable()
export class InterceptInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   
    const token = this.accountService.getToken();

    if (token != null){
      
      request = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`)
        //   setHeaders: {
          //     Authorization: `Bearer ${token}`
          // }
        })
      }
      
      console.log(request);
    return next.handle(request);
  }
}
