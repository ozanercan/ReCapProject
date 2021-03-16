import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CarDetailDto } from 'src/app/models/carDetailDto';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrls: ['./car-card.component.css'],
})
export class CarCardComponent implements OnInit {
  constructor(private router: Router) {}

  @Input() carDetail!: CarDetailDto;

  ngOnInit(): void {}

  navigateToCarDetailPage() {
    window.location.href = '/carDetail/car/' + this.carDetail.id;
  }
}
