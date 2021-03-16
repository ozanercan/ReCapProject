import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarDetailDto } from 'src/app/models/carDetailDto';
import { DataResponseModel } from 'src/app/models/responseModels/dataResponseModel';
import { ResponseModel } from 'src/app/models/responseModels/responseModel';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-list-with-card',
  templateUrl: './car-list-with-card.component.html',
  styleUrls: ['./car-list-with-card.component.css'],
})
export class CarListWithCardComponent implements OnInit {
  constructor(
    private carService: CarService,
    private activatedRoute: ActivatedRoute
  ) {}

  errorResponse!: ResponseModel | undefined;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['brandId']) {
        this.getCarDetailsByBrandId(parameter['brandId']);
      } else if (parameter['colorId']) {
        this.getCarDetailsByColorId(parameter['colorId']);
      } else {
        this.getCarDetails();
      }
      this.errorResponse = undefined;
    });
  }

  carDetails: CarDetailDto[] = [];

  @Input() brandId!: number;

  requestResult!: ResponseModel;

  getCarDetailsByBrandId(brandId: number) {
    this.carService.getCarDetailsByBrandId(brandId).subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }
  getCarDetailsByColorId(colorId: number) {
    this.carService.getCarDetailsByColorId(colorId).subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }
  getCarDetails() {
    this.carService.getCarDetails().subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }
}
