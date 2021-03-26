import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { CustomerDetailDto } from '../models/Dtos/customerDetailDto';
import { CustomerUpdateWithUserDto } from '../models/Dtos/customerUpdateWithUserDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';
import { RememberMeService } from './remember-me.service';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private httpClient: HttpClient) {}

  getCustomerDetailsPath: string = 'customers/getdetailcustomers';
  getCustomerDetailByEmailPath: string = 'customers/getdetailbyemail';
  getUpdateWithUserPath: string = 'customers/updatewithuser';

  getCustomerDetailDtos(): Observable<DataResponseModel<CustomerDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CustomerDetailDto[]>>(
      ApiUrlHelper.getUrl(this.getCustomerDetailsPath)
    );
  }

  getCustomerDetailByEmail(email:string): Observable<DataResponseModel<CustomerDetailDto>> {
    return this.httpClient.get<DataResponseModel<CustomerDetailDto>>(
      ApiUrlHelper.getUrlWithParameters(this.getCustomerDetailByEmailPath, [{key:'email', value:email}])
    );
  }

  updateWithUser(updateWithUserDto:CustomerUpdateWithUserDto): Observable<ResponseModel> {
    return this.httpClient.patch<ResponseModel>(
      ApiUrlHelper.getUrl(this.getUpdateWithUserPath), updateWithUserDto
    );
  }
}
