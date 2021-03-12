import { Component, OnInit } from '@angular/core';
import { CarDto } from 'src/app/models/car/carDto';
import { CarService } from 'src/app/services/car/car.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css'],
})
export class CarListComponent implements OnInit {
  constructor(private carService: CarService) {
    this.getCars();
  }

  carDtos: CarDto[] = [];

  ngOnInit(): void {}

  getCars() {
    this.carService.getCars().subscribe((response) => {
      this.carDtos = response.data;
    });
  }
}
