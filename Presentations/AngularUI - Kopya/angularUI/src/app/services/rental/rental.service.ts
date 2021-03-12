import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConnectionStrings } from 'src/app/constants/apiConnectionStrings';
import { RentalDto } from 'src/app/models/rental/rentalDto';
import { IDataResponse } from 'src/app/models/responseModels/IDataResponse';

@Injectable({
  providedIn: 'root',
})
export class RentalService {
  constructor(private httpClient: HttpClient) {}

  getRentalDtos(): Observable<IDataResponse<RentalDto[]>> {
    return this.httpClient.get<IDataResponse<RentalDto[]>>(
      ApiConnectionStrings.getRentalListDtoUrl
    );
  }
}
