import { Injectable } from '@angular/core';
import { AccessTokenDto } from '../models/Dtos/accessTokenDto';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  cacheKey: string = 'token';

  setToken(accessToken: AccessTokenDto) {
    let json = JSON.stringify(accessToken);
    localStorage.setItem(this.cacheKey, json);
  }

  getToken(): AccessTokenDto {
    let jsonContent = localStorage.getItem(this.cacheKey);
    return JSON.parse(jsonContent!) as AccessTokenDto;
  }

  tokenExist(): boolean {
    if (localStorage.getItem(this.cacheKey)) return true;
    else return false;
  }

  delete() {
    localStorage.removeItem(this.cacheKey);
  }
}
