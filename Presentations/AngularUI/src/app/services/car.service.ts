import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { BrandDto } from '../models/Dtos/brandDto';
import { CarDto } from '../models/Dtos/carDto';
import { CarAddDto } from '../models/Dtos/carAddDto';
import { CarDetailDto } from '../models/Dtos/carDetailDto';
import { CarFilterDto } from '../models/Dtos/carFilterDto';
import { CarImageDto } from '../models/Dtos/carImageDto';
import { ColorAddDto } from '../models/Dtos/colorAddDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';
import { CarUpdateDto } from '../models/Dtos/carUpdateDto';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) {}

  getUrl: string = 'cars/getdetails';

  getUpdateCarPath: string = 'cars/update';

  getByIdUrl: string = 'cars/getbyid';

  getCarAddPath: string = 'cars/add';

  getCarDetailsByBrandIdPath: string = 'cars/getcardetailsbybrandid';

  getCarDetailsByBrandNameIdPath: string = 'cars/getcardetailsbybrandname';

  getCarDetailsByColorIdPath: string = 'cars/getcardetailsbycolorid';

  getCarDetailsByColorNamePath: string = 'cars/getcardetailsbycolorname';

  getCarDetailByCarIdPath: string = 'cars/getcardetailsbycarid';

  getCarDetailByFiltersPath: string = 'cars/getcardetailsbyfilters';

  getCarImagesByCarIdPath: string = 'carimages/getlistbycarid';

  getMoneyToPaidByRentalIdPath: string = 'cars/getcarrentpricebyrentalid';

  addCar(carAddDto: CarAddDto) {
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrl(this.getCarAddPath),
      carAddDto
    );
  }

  update(carUpdateDto: CarUpdateDto): Observable<ResponseModel> {
    if (carUpdateDto !== undefined) {
      return this.httpClient.patch<ResponseModel>(
        ApiUrlHelper.getUrl(this.getUpdateCarPath),
        carUpdateDto
      );
    }

    this.toastrService.error('Lütfen gerekli alanları doldurunuz.');
    throw new Error('');
  }

  getById(id: number): Observable<DataResponseModel<CarDto>> {
    return this.httpClient.get<DataResponseModel<CarDto>>(
      ApiUrlHelper.getUrlWithParameters(this.getByIdUrl, [
        { key: 'id', value: id },
      ])
    );
  }

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
  ): Observable<DataResponseModel<CarImageDto[]>> {
    let url = ApiUrlHelper.getUrlWithParameters(this.getCarImagesByCarIdPath, [
      { key: 'carId', value: carId },
    ]);

    return this.httpClient.get<DataResponseModel<CarImageDto[]>>(url);

    // `${this.getCarImagesByCarIdPath}?carId=${carId}`
  }
}
