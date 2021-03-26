import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { TokenService } from '../services/token.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private tokenService: TokenService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (this.authService.isAuthentication()) {
      let newRequest: HttpRequest<any> = request.clone({
        headers: request.headers.set(
          'Authorization',
          'Bearer ' + this.tokenService.getToken()
        ),
      });

      return next.handle(newRequest);
    } else {
      return next.handle(request);
    }
  }
}
