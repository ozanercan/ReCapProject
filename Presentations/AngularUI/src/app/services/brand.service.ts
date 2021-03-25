import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { BrandDto } from '../models/Dtos/brandDto';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { ResponseModel } from '../models/responseModels/responseModel';
import { ToastrService } from 'ngx-toastr';
import { BrandUpdateDto } from '../models/Dtos/brandUpdateDto';
import { BrandAddDto } from '../models/Dtos/brandAddDto';
@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) {}

  getBrandsPath: string = 'brands/getall';
  getBrandByIdPath: string = 'brands/getbyid';
  getBrandAddPath: string = 'brands/add';
  getBrandUpdatePath: string = 'brands/update';

  getById(id: number): Observable<DataResponseModel<BrandDto>> {
    return this.httpClient.get<DataResponseModel<BrandDto>>(
      ApiUrlHelper.getUrlWithParameters(this.getBrandByIdPath, [
        { key: 'id', value: id },
      ])
    );
  }

  getList(): Observable<DataResponseModel<BrandDto[]>> {
    return this.httpClient.get<DataResponseModel<BrandDto[]>>(
      ApiUrlHelper.getUrl(this.getBrandsPath)
    );
  }

  add(brandAddDto: BrandAddDto): Observable<ResponseModel> {
    if (brandAddDto !== undefined && brandAddDto.name !== undefined) {
      return this.httpClient.post<ResponseModel>(
        ApiUrlHelper.getUrl(this.getBrandAddPath),
        brandAddDto
      );
    }

    this.toastrService.error('Lütfen gerekli alanları doldurunuz.');
    throw new Error('');
  }

  update(brandUpdateDto: BrandUpdateDto): Observable<ResponseModel> {
  
    if (brandUpdateDto !== undefined && brandUpdateDto.name !== undefined) {
      return this.httpClient.patch<ResponseModel>(
        ApiUrlHelper.getUrl(this.getBrandUpdatePath),
        brandUpdateDto
      );
    }

    this.toastrService.error('Lütfen gerekli alanları doldurunuz.');
    throw new Error('');
  }
}
