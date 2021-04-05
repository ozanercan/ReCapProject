import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { GearTypeViewDto } from '../models/Dtos/GearTypeViewDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root'
})
export class GearTypeService {
  constructor(private httpClient: HttpClient) {}

  getAllViewDtoPath: string = 'gearTypes/GetViewList';

  getAllViewDtos(): Observable<DataResponseModel<GearTypeViewDto[]>> {
    return this.httpClient.get<DataResponseModel<GearTypeViewDto[]>>(
      ApiUrlHelper.getUrl(this.getAllViewDtoPath)
    );
  }
}
