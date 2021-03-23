import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { Color } from '../models/color';
import { ColorAddDto } from '../models/colorAddDto';
import { ColorUpdateDto } from '../models/ColorUpdateDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) {}

  getColorByIdPath: string = 'colors/getbyid';
  getColorsPath: string = 'colors/getall';
  getColorAddPath: string = 'colors/add';
  getColorUpdatePath: string = 'colors/update';

  getColors(): Observable<DataResponseModel<Color[]>> {
    return this.httpClient.get<DataResponseModel<Color[]>>(
      ApiUrlHelper.getUrl(this.getColorsPath)
    );
  }

  addColor(colorAddDto: ColorAddDto): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrl(this.getColorAddPath),
      colorAddDto
    );
  }

  getById(id: number): Observable<DataResponseModel<Color>> {
    return this.httpClient.get<DataResponseModel<Color>>(
      ApiUrlHelper.getUrlWithParameters(this.getColorByIdPath, [
        { key: 'id', value: id },
      ])
    );
  }

  update(colorUpdateDto: ColorUpdateDto): Observable<ResponseModel> {
    if (colorUpdateDto !== undefined && colorUpdateDto.name !== undefined) {
      return this.httpClient.patch<ResponseModel>(
        ApiUrlHelper.getUrl(this.getColorUpdatePath),
        colorUpdateDto
      );
    }

    this.toastrService.error('Lütfen gerekli alanları doldurunuz.');
    throw new Error('');
  }
}
