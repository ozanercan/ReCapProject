import { ResponseModel } from './responseModel';

export interface DataResponseModel<T> extends ResponseModel {
  data: T;
}