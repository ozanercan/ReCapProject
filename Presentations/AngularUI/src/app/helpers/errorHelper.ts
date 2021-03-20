import { HttpErrorResponse } from '@angular/common/http';

export class ErrorHelper {
  static getMessage(error: HttpErrorResponse): string {
    if (error.error.detail.length > 1) {
      return error.error.detail;
    } else {
      return error.error.message;
    }
  }
}
