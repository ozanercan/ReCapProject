import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TitleService {
  constructor() {}

  private prefix: string = '';
  private suffix: string = ' | Rental';
  setTitle(title: string) {
    document.title = this.prefix + title + this.suffix;
  }
}
