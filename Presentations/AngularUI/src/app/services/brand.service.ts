import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataResponseModel } from '../models/responseModels/dataResponseModel';
import { Brand } from '../models/brand';
import { Observable } from 'rxjs';
import { ApiUrlHelper } from '../helpers/api-url-helper';
import { BrandAddDto } from '../models/brandAddDto';
import { ResponseModel } from '../models/responseModels/responseModel';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(
    private httpClient: HttpClient,
    private toastrService: ToastrService
  ) {}

  getBrandsPath: string = 'brands/getall';
  getBrandAddPath: string = 'brands/add';

  getBrands(): Observable<DataResponseModel<Brand[]>> {
    return this.httpClient.get<DataResponseModel<Brand[]>>(
      ApiUrlHelper.getUrl(this.getBrandsPath)
    );
  }

  addBrand(brandAddDto: BrandAddDto): Observable<ResponseModel> {
    if (brandAddDto !== undefined && brandAddDto.name !== undefined) {
      return this.httpClient.post<ResponseModel>(
        ApiUrlHelper.getUrl(this.getBrandAddPath),
        brandAddDto
      );
    }

    this.toastrService.error('Lütfen gerekli alanları doldurunuz.');
    throw new Error('');
  }
}
