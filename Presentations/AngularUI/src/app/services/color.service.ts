import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Color } from '../models/color';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  constructor(private httpClient: HttpClient) {}

  getUrl: string = 'https://localhost:5001/api/colors/getall';

  getColors(): Observable<DataResponseModel<Color[]>> {
    return this.httpClient.get<DataResponseModel<Color[]>>(this.getUrl);
  }
}
