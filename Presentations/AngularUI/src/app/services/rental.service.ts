import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Observable } from 'rxjs';
import { RentalDto } from '../models/rentalDto';
import { ResponseModel } from '../models/responseModels/responseModel';
import { RentalCreateDto } from '../models/rentalCreateDto';
import { Rental } from '../models/rental';
@Injectable({
  providedIn: 'root',
})
export class RentalService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/rentals/getdetails';
  newRentalUrl: string = 'https://localhost:5001/api/rentals/add';

  getRentals(): Observable<DataResponseModel<RentalDto[]>> {
    return this.httpClient.get<DataResponseModel<RentalDto[]>>(this.getUrl);
  }

  addRental(rentalCreateDto: RentalCreateDto): Observable<DataResponseModel<Rental>> {
    return this.httpClient.post<DataResponseModel<Rental>>(this.newRentalUrl, rentalCreateDto);
  }
}
