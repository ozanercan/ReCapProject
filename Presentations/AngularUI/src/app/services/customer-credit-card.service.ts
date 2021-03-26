import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { CustomerCreditCardAddDto } from '../models/Dtos/customerCreditCardAddDto';
import { CustomerCreditCardDto } from '../models/Dtos/customerCreditCardDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class CustomerCreditCardService {
  constructor(private httpClient: HttpClient) {}

  getAddPath = 'customercreditcards/add';
  getCardsByCustomerIdPath = 'customercreditcards/getcardsbycustomerid';

  add(
    customerCreditCardAddDto: CustomerCreditCardAddDto
  ): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrl(this.getAddPath),
      customerCreditCardAddDto
    );
  }

  getCardsByCustomerId(
    customerId: number
  ): Observable<DataResponseModel<CustomerCreditCardDto[]>> {
    return this.httpClient.get<DataResponseModel<CustomerCreditCardDto[]>>(
      ApiUrlHelper.getUrlWithParameters(this.getCardsByCustomerIdPath, [
        { key: 'customerId', value: customerId },
      ])
    );
  }
}
