import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-car-update-page',
  templateUrl: './car-update-page.component.html',
  styleUrls: ['./car-update-page.component.css']
})
export class CarUpdatePageComponent implements OnInit {

  constructor(private titleService: TitleService) { }

  ngOnInit(): void {
    this.titleService.setTitle('Araç Güncelle');
  }

}
