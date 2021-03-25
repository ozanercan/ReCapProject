import { Component, OnInit } from '@angular/core';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
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

  colors: ColorDto[] = [];

  currentColor: ColorDto | undefined;

  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }

  setCurrentColor(color: ColorDto) {
    this.currentColor = color;
  }
}
