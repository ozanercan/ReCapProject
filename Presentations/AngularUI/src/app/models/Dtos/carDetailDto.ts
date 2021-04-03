import { CarImageDto } from "./carImageDto";

export class CarDetailDto {
  id!: number;
  brandName!: string;
  colorName!: string;
  modelYear!: number;
  dailyPrice!: number;
  description!: string;
  minCreditScore!: number;

  imagePaths!: CarImageDto[];
}
