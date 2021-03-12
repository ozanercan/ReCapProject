import { IResponse } from './IResponse';

export interface IDataResponse<T> extends IResponse {
  data: T;
}
