import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { CarImage } from '../models/carImage';
import { CarImageDeleteDto } from '../models/Dtos/carImageDeleteDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class CarImageService {
  constructor(private httpClient: HttpClient) {}

  addPrimitivePath: string = 'carimages/addPrimitive';

  getListByCarIdPath: string = 'carimages/getlistbycarid';

  deleteByIdPath: string = 'carimages/deletebyid';

  add(formData: FormData, carId: number): Observable<ResponseModel> {
    let body = formData;
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrlWithParameters(this.addPrimitivePath, [
        { key: 'carId', value: carId },
      ]),
      body
    );
  }

  getListByCarId(carId: number): Observable<DataResponseModel<CarImage[]>> {
    return this.httpClient.get<DataResponseModel<CarImage[]>>(
      ApiUrlHelper.getUrlWithParameters(this.getListByCarIdPath, [
        { key: 'carId', value: carId },
      ])
    );
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(
      ApiUrlHelper.getUrlWithParameters(this.deleteByIdPath, [
        { key: 'id', value: id },
      ])
    );
  }
}
