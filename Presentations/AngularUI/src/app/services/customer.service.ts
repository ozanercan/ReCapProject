import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { CustomerDetailDto } from '../models/customerDetailDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private httpClient: HttpClient) {}

  getCustomerDetailsPath: string = 'customers/getdetailcustomers';

  getCustomerDetailDtos(): Observable<DataResponseModel<CustomerDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CustomerDetailDto[]>>(
      ApiUrlHelper.getUrl(this.getCustomerDetailsPath)
    );
  }
}
