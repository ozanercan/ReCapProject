import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Observable } from 'rxjs';
import { RentalDto } from '../models/rentalDto';
import { RentalCreateDto } from '../models/rentalCreateDto';
import { Rental } from '../models/rental';
import { ApiUrlHelper } from '../helpers/api-url-helper';

@Injectable({
  providedIn: 'root',
})
export class RentalService {
  constructor(private httpClient: HttpClient) {}

  getRentalDetailsPath: string = 'rentals/getdetails';
  rentalAddPath: string = 'rentals/add';

  getRentals(): Observable<DataResponseModel<RentalDto[]>> {
    return this.httpClient.get<DataResponseModel<RentalDto[]>>(
      ApiUrlHelper.getUrl(this.getRentalDetailsPath)
    );
  }

  addRental(
    rentalCreateDto: RentalCreateDto
  ): Observable<DataResponseModel<Rental>> {
    return this.httpClient.post<DataResponseModel<Rental>>(
      ApiUrlHelper.getUrl(this.rentalAddPath),
      rentalCreateDto
    );
  }
}
