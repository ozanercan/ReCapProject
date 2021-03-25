import { HttpClient } from '@angular/common/http';
import { tokenize } from '@angular/compiler/src/ml_parser/lexer';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { AccessTokenDto } from '../models/Dtos/accessTokenDto';
import { LoginDto } from '../models/Dtos/loginDto';
import { RegisterDto } from '../models/Dtos/registerDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService,
    private tokenService: TokenService
  ) {}

  getLoginPath: string = 'auth/login';
  getRegisterPath: string = 'auth/register';

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

  isAuthentication(): boolean {
    return this.tokenService.tokenExist();
  }
}
