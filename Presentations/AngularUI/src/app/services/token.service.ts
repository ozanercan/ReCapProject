import { Injectable } from '@angular/core';
import { AccessTokenDto } from '../models/Dtos/accessTokenDto';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  tokenKey: string = 'token';

  setToken(accessToken: AccessTokenDto) {
    let json = JSON.stringify(accessToken);
    localStorage.setItem(this.tokenKey, json);
  }

  getToken(): AccessTokenDto {
    let jsonContent = localStorage.getItem(this.tokenKey);
    return JSON.parse(jsonContent!) as AccessTokenDto;
  }

  tokenExist(): boolean {
    if (localStorage.getItem(this.tokenKey)) return true;
    else return false;
  }
}
