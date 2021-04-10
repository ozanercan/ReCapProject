import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-car-list-by-color-page',
  templateUrl: './car-list-by-color-page.component.html',
  styleUrls: ['./car-list-by-color-page.component.css']
})
export class CarListByColorPageComponent implements OnInit {

  constructor(private titleService: TitleService) { }

  ngOnInit(): void {
    this.titleService.setTitle('Renge Göre Araç Listesi');
  }

}
