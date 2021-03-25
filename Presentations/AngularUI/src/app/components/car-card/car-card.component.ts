import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css'],
})
export class CarCardComponent implements OnInit {
  constructor() {}

  @Input() carDetail!: CarDetailDto;

  ngOnInit(){
  }

  navigateToCarDetailPage() {
    window.location.href = '/carDetail/car/' + this.carDetail.id;
  }
}
