import { Pipe, PipeTransform } from '@angular/core';
import { ColorDto } from '../models/Dtos/colorDto';

@Pipe({
  name: 'colorFilter',
})
export class ColorFilterPipe implements PipeTransform {
  transform(value: ColorDto[], filterText: string): ColorDto[] {
    let filter = filterText.toLocaleLowerCase();

    let filteredColors: ColorDto[] = value.filter(
      (p) => p.name.toLocaleLowerCase().indexOf(filter) !== -1
    );

    return filteredColors;
  }
}
