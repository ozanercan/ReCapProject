import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { ColorDto } from '../models/Dtos/colorDto';
import { ColorAddDto } from '../models/Dtos/colorAddDto';
import { ColorUpdateDto } from '../models/Dtos/colorUpdateDto';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { ResponseModel } from '../models/responseModels/responseModel';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  constructor(private httpClient: HttpClient, private toastrService:ToastrService) {}

  getColorByIdPath: string = 'colors/getbyid';
  getColorsPath: string = 'colors/getall';
  getColorAddPath: string = 'colors/add';
  getColorUpdatePath: string = 'colors/update';

  getColors(): Observable<DataResponseModel<ColorDto[]>> {
    return this.httpClient.get<DataResponseModel<ColorDto[]>>(
      ApiUrlHelper.getUrl(this.getColorsPath)
    );
  }

  addColor(colorAddDto: ColorAddDto): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(
      ApiUrlHelper.getUrl(this.getColorAddPath),
      colorAddDto
    );
  }

  getById(id: number): Observable<DataResponseModel<ColorDto>> {
    return this.httpClient.get<DataResponseModel<ColorDto>>(
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
