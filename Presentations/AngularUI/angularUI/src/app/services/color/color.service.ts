import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiConnectionStrings } from 'src/app/constants/apiConnectionStrings';
import { Color } from 'src/app/models/color/color';
import { IDataResponse } from 'src/app/models/responseModels/IDataResponse';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  constructor(private httpClient: HttpClient) {}

  getColors(): Observable<IDataResponse<Color[]>> {
    return this.httpClient.get<IDataResponse<Color[]>>(
      ApiConnectionStrings.getColorListUrl
    );
  }
}
