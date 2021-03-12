import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConnectionStrings } from 'src/app/constants/apiConnectionStrings';
import { Customer } from 'src/app/models/customer/customer';
import { IDataResponse } from 'src/app/models/responseModels/IDataResponse';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private httpClient: HttpClient) {}

  getCustomers(): Observable<IDataResponse<Customer[]>> {
    return this.httpClient.get<IDataResponse<Customer[]>>(
      ApiConnectionStrings.getUserListUrl
    );
  }
}
