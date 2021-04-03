import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: 'input[onlyNumbers]'
})
export class OnlyNumbersDirective {

  constructor(private elementRef: ElementRef) { }

  @HostListener('input', ['$event']) onInputChange(event:any) {
    const initalValue = this.elementRef.nativeElement.value;
    this.elementRef.nativeElement.value = initalValue.replace(/[^0-9]*/g, '');
    if ( initalValue !== this.elementRef.nativeElement.value) {
      event.stopPropagation();
    }
  }

}
