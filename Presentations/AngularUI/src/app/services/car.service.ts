import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand } from '../models/brand';
import { CarDetailDto } from '../models/carDetailDto';
import { CarImage } from '../models/carImage';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/cars/getdetails';

  getCarDetailsByBrandUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbybrandid';

  getCarDetailsByColorUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbycolorid';

  getCarDetailByCarUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbycarid';

  getCarImagesByColorUrl: string =
    'https://localhost:5001/api/carimages/getlistbycarid';

  getCarDetails(): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(this.getUrl);
  }

  getCarDetailsByBrandId(
    brandId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByBrandUrl}?brandId=${brandId}`
    );
  }

  getCarDetailsByColorId(
    colorId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByColorUrl}?colorId=${colorId}`
    );
  }

  getCarDetailByCarId(
    carId: number
  ): Observable<DataResponseModel<CarDetailDto>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto>>(
      `${this.getCarDetailByCarUrl}?carId=${carId}`
    );
  }

  getCarImagesByCarId(
    carId: number
  ): Observable<DataResponseModel<CarImage[]>> {
    return this.httpClient.get<DataResponseModel<CarImage[]>>(
      `${this.getCarImagesByColorUrl}?carId=${carId}`
    );
  }
}
