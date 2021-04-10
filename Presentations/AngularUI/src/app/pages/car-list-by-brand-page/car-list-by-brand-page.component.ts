import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-car-list-by-brand-page',
  templateUrl: './car-list-by-brand-page.component.html',
  styleUrls: ['./car-list-by-brand-page.component.css'],
})
export class CarListByBrandPageComponent implements OnInit {
  constructor(private titleService: TitleService) {}

  ngOnInit(): void {
    this.titleService.setTitle('Markaya Göre Araç Listesi');
  }
}
