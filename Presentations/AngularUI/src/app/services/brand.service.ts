import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Brand } from '../models/brand';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private httpClient: HttpClient) {}

  getBrandsPath: string = 'brands/getall';

  getBrands(): Observable<DataResponseModel<Brand[]>> {
    return this.httpClient.get<DataResponseModel<Brand[]>>(
      ApiUrlHelper.getUrl(this.getBrandsPath)
    );
  }
}
