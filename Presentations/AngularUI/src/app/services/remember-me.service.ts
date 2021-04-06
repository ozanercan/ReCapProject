import { Injectable } from '@angular/core';
import { User } from '../models/Dtos/user';

@Injectable({
  providedIn: 'root',
})
export class RememberMeService {
  constructor() {}

  emailCacheKey = 'loggedInUserEmail';
  userCacheKey = 'user';

  setEmail(email: string) {
    localStorage.setItem(this.emailCacheKey, email);
  }
  getEmail(): string | null {
    return localStorage.getItem(this.emailCacheKey);
  }

  deleteEmail() {
    localStorage.removeItem(this.emailCacheKey);
  }

  setUser(user: User) {
    localStorage.setItem(this.userCacheKey, JSON.stringify(user));
  }
  getUser(): User {
    let user: User = JSON.parse(localStorage.getItem(this.userCacheKey)!);

    return user;
  }

  deleteUser() {
    localStorage.removeItem(this.userCacheKey);
  }
}
