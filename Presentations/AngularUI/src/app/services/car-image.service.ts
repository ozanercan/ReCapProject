import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class CarImageService {
  constructor(private httpClient: HttpClient) {}

  add(formData: FormData, carId: number): Observable<ResponseModel> {
    let body = formData;
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrl('carimages/addPrimitive?carId=' + carId),
      body
    );
  }
}
