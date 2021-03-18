import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Brand } from '../models/brand';
import { CarDetailDto } from '../models/carDetailDto';
import { CarFilterDto } from '../models/carFilterDto';
import { CarImage } from '../models/carImage';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/cars/getdetails';

  getCarDetailsByBrandUrlIdUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbybrandid';

  getCarDetailsByBrandNameIdUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbybrandname';

  getCarDetailsByColorIdUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbycolorid';

  getCarDetailsByColorNameUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbycolorname';

  getCarDetailByCarIdUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbycarid';

  getCarDetailByFiltersUrl: string =
    'https://localhost:5001/api/cars/getcardetailsbyfilters';

  getCarImagesByCarIdUrl: string =
    'https://localhost:5001/api/carimages/getlistbycarid';

  getCarDetails(): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(this.getUrl);
  }

  getCarDetailsByBrandId(
    brandId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByBrandUrlIdUrl}?brandId=${brandId}`
    );
  }

  getCarDetailsByBrandName(
    brandName: string
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByBrandNameIdUrl}?brandName=${brandName}`
    );
  }

  getCarDetailsByColorId(
    colorId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByColorIdUrl}?colorId=${colorId}`
    );
  }

  getCarDetailsByColorName(
    colorName: string
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailsByColorNameUrl}?colorName=${colorName}`
    );
  }

  getCarDetailByCarId(
    carId: number
  ): Observable<DataResponseModel<CarDetailDto>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto>>(
      `${this.getCarDetailByCarIdUrl}?carId=${carId}`
    );
  }

  getCarDetailsByFilters(
    carFilterDto: CarFilterDto
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.post<DataResponseModel<CarDetailDto[]>>(
      `${this.getCarDetailByFiltersUrl}`, carFilterDto
    );
  }

  getCarImagesByCarId(
    carId: number
  ): Observable<DataResponseModel<CarImage[]>> {
    return this.httpClient.get<DataResponseModel<CarImage[]>>(
      `${this.getCarImagesByCarIdUrl}?carId=${carId}`
    );
  }
}
