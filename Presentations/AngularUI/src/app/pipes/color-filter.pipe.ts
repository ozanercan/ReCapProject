import { Pipe, PipeTransform } from '@angular/core';
import { Color } from '../models/color';

@Pipe({
  name: 'colorFilter',
})
export class ColorFilterPipe implements PipeTransform {
  transform(value: Color[], filterText: string): Color[] {
    let filter = filterText.toLocaleLowerCase();

    let filteredColors: Color[] = value.filter(
      (p) => p.name.toLocaleLowerCase().indexOf(filter) !== -1
    );

    return filteredColors;
  }
}
