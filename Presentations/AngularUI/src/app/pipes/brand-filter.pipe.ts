import { Pipe, PipeTransform } from '@angular/core';
import { BrandDto } from '../models/Dtos/brandDto';

@Pipe({
  name: 'brandFilter',
})
export class BrandFilterPipe implements PipeTransform {
  transform(value: BrandDto[], filterText: string): BrandDto[] {
    let filter = filterText.toLocaleLowerCase();

    return value.filter(
      (p) => p.name.toLocaleLowerCase().indexOf(filter) !== -1
    );
  }
}
