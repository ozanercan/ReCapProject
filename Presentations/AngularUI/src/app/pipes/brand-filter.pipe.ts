import { Pipe, PipeTransform } from '@angular/core';
import { Brand } from '../models/brand';

@Pipe({
  name: 'brandFilter',
})
export class BrandFilterPipe implements PipeTransform {
  transform(value: Brand[], filterText: string): Brand[] {
    let filter = filterText.toLocaleLowerCase();

    return value.filter(
      (p) => p.name.toLocaleLowerCase().indexOf(filter) !== -1
    );
  }
}
