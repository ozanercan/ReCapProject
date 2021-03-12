import { Component, OnInit } from '@angular/core';
import { Color } from 'src/app/models/color/color';
import { ColorService } from 'src/app/services/color/color.service';

@Component({
  selector: 'app-color-list',
  templateUrl: './color-list.component.html',
  styleUrls: ['./color-list.component.css'],
})
export class ColorListComponent implements OnInit {
  constructor(private colorService: ColorService) {
    this.getColors();
  }

  ngOnInit(): void {}

  colors: Color[] = [];

  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }
}
