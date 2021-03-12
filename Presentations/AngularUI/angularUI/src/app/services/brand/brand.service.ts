import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IDataResponse } from 'src/app/models/responseModels/IDataResponse';
import { Observable } from 'rxjs';
import { Brand } from 'src/app/models/brand/brand';
import { ApiConnectionStrings } from 'src/app/constants/apiConnectionStrings';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private httpClient: HttpClient) {}

  getBrands(): Observable<IDataResponse<Brand[]>> {
    return this.httpClient.get<IDataResponse<Brand[]>>(
      ApiConnectionStrings.getBrandListUrl
    );
  }
}
