import { Pipe, PipeTransform } from '@angular/core';
import { CarDetailDto } from '../models/Dtos/carDetailDto';

@Pipe({
  name: 'carFilter',
})
export class CarFilterPipe implements PipeTransform {

  transform(value: CarDetailDto[], filterText: string): CarDetailDto[] {
    let filter = filterText.toLocaleLowerCase();

    let filteredBrands = value.filter(
      (p) => p.brandName.toLocaleLowerCase().indexOf(filter) !== -1
    );
    if (filteredBrands.length > 0) return filteredBrands;

    let filteredColors = value.filter(
      (p) => p.colorName.toLocaleLowerCase().indexOf(filter) !== -1
    );
    if (filteredColors.length > 0) return filteredColors;

    let filteredDescriptions = value.filter(
      (p) => p.description.toLocaleLowerCase().indexOf(filter) !== -1
    );
    if (filteredDescriptions.length > 0) return filteredDescriptions;

    let filteredModelYears = value.filter((p) =>
      p.modelYear.toString().startsWith(filter)
    );
    if (filteredModelYears.length > 0) return filteredModelYears;

    let filteredDailyPrice = value.filter((p) =>
      p.dailyPrice.toString().startsWith(filter)
    );
    if (filteredDailyPrice.length > 0) return filteredDailyPrice;

    return value;
  }
}
