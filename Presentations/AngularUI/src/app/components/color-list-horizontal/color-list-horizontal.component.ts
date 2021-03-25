import { Component, OnInit } from '@angular/core';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
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
