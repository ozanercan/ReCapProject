import { Component, ErrorHandler, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
import { CarFilterDto } from 'src/app/models/Dtos/carFilterDto';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
import { FuelTypeViewDto } from 'src/app/models/Dtos/fuelTypeViewDto';
import { GearTypeViewDto } from 'src/app/models/Dtos/GearTypeViewDto';
import { BrandService } from 'src/app/services/brand.service';
import { ColorService } from 'src/app/services/color.service';
import { FuelTypeService } from 'src/app/services/fuel-type.service';
import { GearTypeService } from 'src/app/services/gear-type.service';

@Component({
  selector: 'app-car-list-by-parameters-page',
  templateUrl: './car-list-by-parameters-page.component.html',
  styleUrls: ['./car-list-by-parameters-page.component.css'],
})
export class CarListByParametersPageComponent implements OnInit {
  constructor(
    private colorService: ColorService,
    private brandService: BrandService,
    private toastrService: ToastrService,
    private fuelTypeService: FuelTypeService,
    private gearTypeService: GearTypeService,
    private activatedRoute: ActivatedRoute
  ) {}

  colors: ColorDto[] = [];
  brands: BrandDto[] = [];
  gearTypes: GearTypeViewDto[] = [];
  fuelTypes: FuelTypeViewDto[] = [];

  selectedBrand!: string;
  selectedColor!: string;
  selectedGearType!: string;
  selectedFuelType!: string;

  ngOnInit(): void {

    this.getColors();
    this.getBrands();
    this.getGearTypes();
    this.getFuelTypes();

    this.activatedRoute.queryParamMap.subscribe((queryParam) => {
      let carFilterDto: CarFilterDto = {
        brandName: queryParam.get('brandName')!,
        colorName: queryParam.get('colorName')!,
        fuelTypeName: queryParam.get('fuelTypeName')!,
        gearTypeName: queryParam.get('gearTypeName')!,
      };

      this.selectedBrand = carFilterDto.brandName;
      this.selectedColor = carFilterDto.colorName;
      this.selectedFuelType = carFilterDto.fuelTypeName;
      this.selectedGearType = carFilterDto.gearTypeName;
    });
  }

  getColors() {
    this.colorService.getColors().subscribe(
      (p) => {
        this.colors = p.data;
      },
      (errorResponse) => {
        this.toastrService.error(ErrorHelper.getMessage(errorResponse));
      }
    );
  }

  getBrands() {
    this.brandService.getList().subscribe(
      (p) => {
        this.brands = p.data;
      },
      (errorResponse) => {
        this.toastrService.error(ErrorHelper.getMessage(errorResponse));
      }
    );
  }

  getFuelTypes() {
    this.fuelTypeService.getAllViewDtos().subscribe(
      (p) => {
        this.fuelTypes = p.data;
      },
      (errorResponse) => {
        this.toastrService.error(ErrorHelper.getMessage(errorResponse));
      }
    );
  }

  getGearTypes() {
    this.gearTypeService.getAllViewDtos().subscribe(
      (p) => {
        this.gearTypes = p.data;
      },
      (errorResponse) => {
        this.toastrService.error(ErrorHelper.getMessage(errorResponse));
      }
    );
  }
}
