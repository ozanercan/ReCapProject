import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-banner',
  templateUrl: './car-banner.component.html',
  styleUrls: ['./car-banner.component.css'],
})
export class CarBannerComponent implements OnInit {
  constructor(
    private carService: CarService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.carService.getCarDetails().subscribe(
      (p) => {
        this.carDetailDtos = p.data;
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error));
      }
    );
  }

  carDetailDtos!: CarDetailDto[];
}
