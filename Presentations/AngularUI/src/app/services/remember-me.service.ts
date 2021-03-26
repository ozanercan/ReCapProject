import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RememberMeService {
  constructor() {}

  cacheKey = 'loggedInUserEmail';

  setEmail(email: string) {
    localStorage.setItem(this.cacheKey, email);
  }
  getEmail(): string | null {
    return localStorage.getItem(this.cacheKey);
  }
}
