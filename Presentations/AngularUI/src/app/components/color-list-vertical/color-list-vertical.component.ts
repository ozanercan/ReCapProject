import { Component, OnInit } from '@angular/core';
import { Color } from 'src/app/models/color';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-list-vertical',
  templateUrl: './color-list-vertical.component.html',
  styleUrls: ['./color-list-vertical.component.css'],
})
export class ColorListVerticalComponent implements OnInit {
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
