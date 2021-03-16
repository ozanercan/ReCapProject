import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerDetailDto } from '../models/customerDetailDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/customers/getdetailcustomers';

  getColors(): Observable<DataResponseModel<CustomerDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CustomerDetailDto[]>>(
      this.getUrl
    );
  }
}
