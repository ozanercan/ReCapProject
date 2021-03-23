import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Brand } from '../models/brand';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { BrandAddDto } from '../models/brandAddDto';
import { ResponseModel } from '../models/responseModels/responseModel';
import { ToastrService } from 'ngx-toastr';
import { BrandUpdateDto } from '../models/brandUpdateDto';
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

  getById(id: number): Observable<DataResponseModel<Brand>> {
    return this.httpClient.get<DataResponseModel<Brand>>(
      ApiUrlHelper.getUrlWithParameters(this.getBrandByIdPath, [
        { key: 'id', value: id },
      ])
    );
  }

  getList(): Observable<DataResponseModel<Brand[]>> {
    return this.httpClient.get<DataResponseModel<Brand[]>>(
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
