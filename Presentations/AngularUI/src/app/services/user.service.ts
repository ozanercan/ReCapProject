import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { Dictionary } from '../models/dictionary';
import { CustomerFirstLastNameDto } from '../models/Dtos/customerFirstLastNameDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) {}

  getFirstLastNameByEmailPath = 'users/getfirstlastnamebyemail';
  
  getFirstLastNameByEmail(email: string) : Observable<DataResponseModel<CustomerFirstLastNameDto>>{
   return this.httpClient.get<DataResponseModel<CustomerFirstLastNameDto>>(
      ApiUrlHelper.getUrlWithParameters(this.getFirstLastNameByEmailPath, [
        { key: 'email', value: email },
      ])
    );
  }
}
