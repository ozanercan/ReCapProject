import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { Color } from '../models/color';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  constructor(private httpClient: HttpClient) {}

  getColorsPath: string = 'colors/getall';

  getColors(): Observable<DataResponseModel<Color[]>> {
    return this.httpClient.get<DataResponseModel<Color[]>>(
      ApiUrlHelper.getUrl(this.getColorsPath)
    );
  }
}
