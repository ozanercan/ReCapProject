import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaymentAddDto } from '../models/paymentAddDto';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private httpClient: HttpClient) {}

  addUrl: string = 'https://localhost:5001/api/payments/add';

  addPayment(paymentAddDto: PaymentAddDto): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.addUrl, paymentAddDto);
  }
}
