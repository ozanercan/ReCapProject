import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { PaymentAddDto } from '../models/Dtos/paymentAddDto';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private httpClient: HttpClient) {}

  paymentAddPath: string = 'payments/add';

  addPayment(paymentAddDto: PaymentAddDto): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(ApiUrlHelper.getUrl(this.paymentAddPath), paymentAddDto);
  }
}
