import { CarImage } from "./carImage";

export class CarDetailDto {
  id!: number;
  brandName!: string;
  colorName!: string;
  modelYear!: number;
  dailyPrice!: number;
  description!: string;

  imagePaths!: CarImage[];
}
