import { Component, OnInit } from '@angular/core';
import { Color } from 'src/app/models/color';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-list-horizontal',
  templateUrl: './color-list-horizontal.component.html',
  styleUrls: ['./color-list-horizontal.component.css'],
})
export class ColorListHorizontalComponent implements OnInit {
  constructor(private colorService: ColorService) {
    this.getColors();
  }

  ngOnInit(): void {}

  colors: Color[] = [];

  currentColor: Color | undefined;

  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }

  setCurrentColor(color: Color) {
    this.currentColor = color;
  }
}
