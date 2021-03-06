import { HttpClient } from '@angular/common/http';
import { tokenize } from '@angular/compiler/src/ml_parser/lexer';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, timer } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { AccessTokenDto } from '../models/Dtos/accessTokenDto';
import { LoginDto } from '../models/Dtos/loginDto';
import { RegisterDto } from '../models/Dtos/registerDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { RememberMeService } from './remember-me.service';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService,
    private tokenService: TokenService,
    private rememberMeService: RememberMeService,
    private router: Router
  ) {}

  getLoginPath: string = 'auth/Login';
  getRegisterPath: string = 'auth/Register';

  login(loginModel: LoginDto): Observable<DataResponseModel<AccessTokenDto>> {
    return this.httpClient.post<DataResponseModel<AccessTokenDto>>(
      ApiUrlHelper.getUrl(this.getLoginPath),
      loginModel
    );
  }

  register(
    registerModel: RegisterDto
  ): Observable<DataResponseModel<AccessTokenDto>> {
    return this.httpClient.post<DataResponseModel<AccessTokenDto>>(
      ApiUrlHelper.getUrl(this.getRegisterPath),
      registerModel
    );
  }

  isUserHaveClaims(necessaryClaims: string[]): boolean {
    if (necessaryClaims == undefined) return false;
    
    let isUserHaveClaim: boolean = false;
    this.tokenService.getToken().claims.forEach((ownedClaim) => {
      necessaryClaims.forEach((necessaryClaim) => {
        if (ownedClaim === 'admin' || ownedClaim === necessaryClaim) {
          isUserHaveClaim = true;
        }
      });
    });

    return isUserHaveClaim;
  }

  isAuthentication(): boolean {
    return this.tokenService.tokenExist();
  }

  logout() {
    this.tokenService.delete();
    this.rememberMeService.deleteEmail();
    this.rememberMeService.deleteUser();
    this.toastrService.success('Başarıyla çıkış yaptınız.');
    timer(1000).subscribe(p=>{
      window.location.reload();
    });
  }
}
