import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConnectionStrings } from 'src/app/constants/apiConnectionStrings';
import { CarDto } from 'src/app/models/car/carDto';
import { IDataResponse } from 'src/app/models/responseModels/IDataResponse';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private httpClient: HttpClient) {}

  getCars(): Observable<IDataResponse<CarDto[]>> {
    return this.httpClient.get<IDataResponse<CarDto[]>>(
      ApiConnectionStrings.getCarListDtoUrl
    );
  }
}
