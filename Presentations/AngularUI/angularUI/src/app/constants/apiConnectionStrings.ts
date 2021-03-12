export class ApiConnectionStrings {
  private static baseUrl: string = 'https://localhost:5001/';
  private static prefix: string = 'api/';

  static getBrandListUrl: string = `${ApiConnectionStrings.baseUrl}${ApiConnectionStrings.prefix}brands/getall`;

  static getCarListDtoUrl: string = `${ApiConnectionStrings.baseUrl}${ApiConnectionStrings.prefix}cars/getalldto`;

  static getColorListUrl: string = `${ApiConnectionStrings.baseUrl}${ApiConnectionStrings.prefix}colors/getall`;

  static getUserListUrl: string = `${ApiConnectionStrings.baseUrl}${ApiConnectionStrings.prefix}customers/getdetailcustomers`;

  static getRentalListDtoUrl: string = `${ApiConnectionStrings.baseUrl}${ApiConnectionStrings.prefix}rentals/getalldto`;


}
