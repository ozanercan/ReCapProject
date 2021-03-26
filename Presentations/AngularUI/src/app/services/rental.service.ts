import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Observable } from 'rxjs';
import { RentalDto } from '../models/Dtos/rentalDto';
import { RentalCreateDto } from '../models/Dtos/rentalCreateDto';
import { ApiUrlHelper } from '../helpers/api-url-helper';

@Injectable({
  providedIn: 'root',
})
export class RentalService {
  constructor(private httpClient: HttpClient) {}

  getRentalDetailsPath: string = 'rentals/getdetails';
  getCustomerIdByIdPath: string = 'rentals/getcustomeridbyid';
  rentalAddPath: string = 'rentals/add';

  getRentals(): Observable<DataResponseModel<RentalDto[]>> {
    return this.httpClient.get<DataResponseModel<RentalDto[]>>(
      ApiUrlHelper.getUrl(this.getRentalDetailsPath)
    );
  }

  getCustomerIdById(id:number): Observable<DataResponseModel<number>> {
    return this.httpClient.get<DataResponseModel<number>>(
      ApiUrlHelper.getUrlWithParameters(this.getCustomerIdByIdPath, [{key:'id', value:id}])
    );
  }

  addRental(
    rentalCreateDto: RentalCreateDto
  ): Observable<DataResponseModel<RentalDto>> {
    return this.httpClient.post<DataResponseModel<RentalDto>>(
      ApiUrlHelper.getUrl(this.rentalAddPath),
      rentalCreateDto
    );
  }
}
