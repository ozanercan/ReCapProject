import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Observable } from 'rxjs';
import { RentalDto } from '../models/rentalDto';
@Injectable({
  providedIn: 'root',
})
export class RentalService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/rentals/getdetails';

  getRentals(): Observable<DataResponseModel<RentalDto[]>> {
    return this.httpClient.get<DataResponseModel<RentalDto[]>>(this.getUrl);
  }
}
