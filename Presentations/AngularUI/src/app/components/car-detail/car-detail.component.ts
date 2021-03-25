import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';
import { CarImageDto } from 'src/app/models/Dtos/carImageDto';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.css'],
})
export class CarDetailComponent implements OnInit {
  constructor(
    private carService: CarService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['carId']) {
        this.getCarDetailById(parameter['carId']);
        this.getCarImagesByCarId(parameter['carId']);
      }
    });
  }

  carImages!: CarImageDto[];
  carDetailDto!: CarDetailDto;

  parentImageUrl!: string;

  getCarDetailById(carId: number) {
    this.carService.getCarDetailByCarId(carId).subscribe((response) => {
      this.carDetailDto = response.data;
    });
  }

  getCarImagesByCarId(carId: number) {
    this.carService.getCarImagesByCarId(carId).subscribe((response) => {
      this.carImages = response.data;

      this.parentImageUrl = this.carImages[0].imagePath;
    });
  }

  getCarouselItemClass(carImage: CarImageDto): string {
    let defaultClass: string = 'carousel-item';
    if (this.carImages.sort((p) => p.id)[0].id == carImage.id) {
      return `${defaultClass} active`;
    }
    return defaultClass;
  }

  setParentImage(carImage: CarImageDto) {
    this.parentImageUrl = carImage.imagePath;
  }

  chooseOtherPhotoByImageUrl(imageUrl: string) {
    var sortedImages = this.carImages.sort((p) => p.id);
    for (let i = 0; i < sortedImages.length; i++) {
      const element = sortedImages[i];
      if (imageUrl == element.imagePath) {
        if (i == this.carImages.length - 1) {
          i = 0;
        } else {
          i++;
        }
        this.setParentImage(sortedImages[i]);
        break;
      }
    }
  }
}
