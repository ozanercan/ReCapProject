import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Brand } from '../models/brand';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/brands/getall';
  
  getBrands(): Observable<DataResponseModel<Brand[]>> {
    return this.httpClient.get<DataResponseModel<Brand[]>>(this.getUrl);
  }
}
