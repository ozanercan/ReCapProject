import { Component, OnInit } from '@angular/core';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-list-with-table',
  templateUrl: './color-list-with-table.component.html',
  styleUrls: ['./color-list-with-table.component.css'],
})
export class ColorListWithTableComponent implements OnInit {
  constructor(private colorService: ColorService) {
  }

  ngOnInit(): void {
    this.getColors();
  }

  colors: ColorDto[] = [];
  filterText: string = '';

  getColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response.data;
    });
  }
}
