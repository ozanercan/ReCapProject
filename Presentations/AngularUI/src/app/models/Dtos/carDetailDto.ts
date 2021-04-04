import { CarImageDto } from "./carImageDto";

export class CarDetailDto {
  id!: number;
  brandName!: string;
  colorName!: string;
  fuelTypeName!: string;
  gearTypeName!: string;
  modelYear!: number;
  horsePower!: number;
  name!: string;
  dailyPrice!: number;
  description!: string;
  minCreditScore!: number;

  imagePaths!: CarImageDto[];
}
