import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { FuelTypeViewDto } from '../models/Dtos/fuelTypeViewDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class FuelTypeService {
  constructor(private httpClient: HttpClient) {}

  getAllViewDtoPath: string = 'fuelTypes/GetViewList';

  getAllViewDtos(): Observable<DataResponseModel<FuelTypeViewDto[]>> {
    return this.httpClient.get<DataResponseModel<FuelTypeViewDto[]>>(
      ApiUrlHelper.getUrl(this.getAllViewDtoPath)
    );
  }
}
