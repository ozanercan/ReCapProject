import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CarImage } from 'src/app/models/carImage';
import { CarImageService } from 'src/app/services/car-image.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-car-image-update-with-form',
  templateUrl: './car-image-update-with-form.component.html',
  styleUrls: ['./car-image-update-with-form.component.css'],
})
export class CarImageUpdateWithFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private carImageService: CarImageService,
    private toastrService: ToastrService,
    private titleService: TitleService
  ) {}

  carId!: number;

  carImages!: CarImage[];

  selectedCarImage!: CarImage;

  ngOnInit(): void {
    this.titleService.setTitle('Fotoğraf Güncelle');
    this.activatedRoute.params.subscribe((param) => {
      if (param['carId']) {
        this.carId = parseInt(param['carId']);
        this.getCarImagesByCarId(this.carId);
      }
    });
  }

  getCarImagesByCarId(carId: number) {
    this.carImageService.getListByCarId(carId).subscribe(
      (response) => {
        this.carImages = response.data;
      },
      (responseError) => {
        this.toastrService.error(ErrorHelper.getMessage(responseError));
      }
    );
  }

  deleteCarImage(carImage: CarImage) {
    console.log(carImage);
    this.carImageService.delete(carImage.id).subscribe(
      (response) => {
        this.toastrService.success(response.message);

        this.getCarImagesByCarId(this.carId);
      },
      (responseError) => {
        console.log(responseError);

        this.toastrService.error(ErrorHelper.getMessage(responseError));
      }
    );
  }
}
