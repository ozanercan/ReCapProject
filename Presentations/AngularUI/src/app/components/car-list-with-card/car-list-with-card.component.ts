import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';
import { CarFilterDto } from 'src/app/models/Dtos/carFilterDto';
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
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private router: ToastrService
  ) {}

  errorResponse!: ResponseModel | undefined;

  ngOnInit(): void {
    this.activatedRoute.queryParamMap.subscribe((queryParam) => {
      console.log(queryParam);
      let carFilterDto: CarFilterDto = {
        brandName: queryParam.get('brandName')!,
        colorName: queryParam.get('colorName')!,
        fuelTypeName: queryParam.get('fuelTypeName')!,
        gearTypeName: queryParam.get('gearTypeName')!
      };
      this.getCarDetailsByFilters(carFilterDto);
    });

    // ESKİ FİLTRELEME KOMUTLARI
    // this.activatedRoute.params.subscribe((parameter) => {
    //   console.log('params');
    //   console.log(parameter);

    //   if (parameter['brandName'] && parameter['colorName']) {
    //     this.getCarDetailsByFilters(
    //       parameter['colorName'],
    //       parameter['brandName']
    //     );
    //   } else if (parameter['brandId']) {
    //     this.getCarDetailsByBrandId(parameter['brandId']);
    //   } else if (parameter['colorId']) {
    //     this.getCarDetailsByColorId(parameter['colorId']);
    //   } else if (parameter['colorName']) {
    //     this.getCarDetailsByColorName(parameter['colorName']);
    //   } else if (parameter['brandName']) {
    //     this.getCarDetailsByBrandName(parameter['brandName']);
    //   } else {
    //     this.getCarDetails();
    //   }
    //   this.errorResponse = undefined;
    // }
    //);
  }

  carDetails: CarDetailDto[] = [];

  @Input() brandId!: number;

  requestResult!: ResponseModel;

  getCarDetailsByColorName(colorName: string) {
    this.carService.getCarDetailsByColorName(colorName).subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }
  getCarDetailsByBrandName(brandName: string) {
    this.carService.getCarDetailsByBrandName(brandName).subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }

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

  getCarDetailsByFilters(carFilterDto: CarFilterDto) {
    this.carService.getCarDetailsByFilters(carFilterDto).subscribe(
      (response) => {
        this.carDetails = response.data;
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
        this.errorResponse = error.error;

        this.carDetails = [];
      }
    );
  }
}
