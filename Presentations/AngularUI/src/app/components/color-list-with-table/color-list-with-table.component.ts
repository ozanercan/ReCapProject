import { Component, OnInit } from '@angular/core';
import { Color } from 'src/app/models/color';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-list-with-table',
  templateUrl: './color-list-with-table.component.html',
  styleUrls: ['./color-list-with-table.component.css'],
})
export class ColorListWithTableComponent implements OnInit {
  constructor(private colorService: ColorService) {
    this.getColors();
  }

  ngOnInit(): void {}

  colors: Color[] = [];
  filterText: string = '';
  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }
}
