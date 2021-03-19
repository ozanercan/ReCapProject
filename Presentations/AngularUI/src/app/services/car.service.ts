import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
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

  getUrl: string = 'cars/getdetails';

  getCarDetailsByBrandIdPath: string = 'cars/getcardetailsbybrandid';

  getCarDetailsByBrandNameIdPath: string = 'cars/getcardetailsbybrandname';

  getCarDetailsByColorIdPath: string = 'cars/getcardetailsbycolorid';

  getCarDetailsByColorNamePath: string = 'cars/getcardetailsbycolorname';

  getCarDetailByCarIdPath: string = 'cars/getcardetailsbycarid';

  getCarDetailByFiltersPath: string = 'cars/getcardetailsbyfilters';

  getCarImagesByCarIdPath: string = 'carimages/getlistbycarid';

  getMoneyToPaidByRentalIdPath: string = 'cars/getcarrentpricebyrentalid';

  getCarDetails(): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(
      ApiUrlHelper.getUrl(this.getUrl)
    );
  }

  GetMoneyToPaidByRentalId(
    rentalId: number
  ): Observable<DataResponseModel<number>> {
    return this.httpClient.get<DataResponseModel<number>>(
      ApiUrlHelper.getUrlWithParameters(this.getMoneyToPaidByRentalIdPath, [
        { key: 'rentalId', value: rentalId },
      ])
    );
  }

  getCarDetailsByBrandId(
    brandId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(
      this.getCarDetailsByBrandIdPath,
      [{ key: 'brandId', value: brandId }]
    );

    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(url);

    //`${this.getCarDetailsByBrandIdPath}?brandId=${brandId}`
  }

  getCarDetailsByBrandName(
    brandName: string
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(
      this.getCarDetailsByBrandNameIdPath,
      [{ key: 'brandName', value: brandName }]
    );

    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(url);

    //`${this.getCarDetailsByBrandNameIdPath}?brandName=${brandName}`
  }

  getCarDetailsByColorId(
    colorId: number
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(
      this.getCarDetailsByColorIdPath,
      [{ key: 'colorId', value: colorId }]
    );

    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(url);
    //`${this.getCarDetailsByColorIdPath}?colorId=${colorId}`
  }

  getCarDetailsByColorName(
    colorName: string
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(
      this.getCarDetailsByColorNamePath,
      [{ key: 'colorName', value: colorName }]
    );

    return this.httpClient.get<DataResponseModel<CarDetailDto[]>>(url);

    //`${this.getCarDetailsByColorNamePath}?colorName=${colorName}`
  }

  getCarDetailByCarId(
    carId: number
  ): Observable<DataResponseModel<CarDetailDto>> {
    let url = ApiUrlHelper.getUrlWithParameters(this.getCarDetailByCarIdPath, [
      { key: 'carId', value: carId },
    ]);

    return this.httpClient.get<DataResponseModel<CarDetailDto>>(url);

    // `${this.getCarDetailByCarIdPath}?carId=${carId}`
  }

  getCarDetailsByFilters(
    carFilterDto: CarFilterDto
  ): Observable<DataResponseModel<CarDetailDto[]>> {
    return this.httpClient.post<DataResponseModel<CarDetailDto[]>>(
      ApiUrlHelper.getUrl(this.getCarDetailByFiltersPath),
      carFilterDto
    );
  }

  getCarImagesByCarId(
    carId: number
  ): Observable<DataResponseModel<CarImage[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(this.getCarImagesByCarIdPath, [
      { key: 'carId', value: carId },
    ]);

    return this.httpClient.get<DataResponseModel<CarImage[]>>(url);

    // `${this.getCarImagesByCarIdPath}?carId=${carId}`
  }
}
