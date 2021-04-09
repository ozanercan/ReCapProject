import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { Car } from 'src/app/models/car';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
import { CarAddDto } from 'src/app/models/Dtos/carAddDto';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
import { FuelTypeViewDto } from 'src/app/models/Dtos/fuelTypeViewDto';
import { GearTypeViewDto } from 'src/app/models/Dtos/GearTypeViewDto';
import { BrandService } from 'src/app/services/brand.service';
import { CarService } from 'src/app/services/car.service';
import { ColorService } from 'src/app/services/color.service';
import { FuelTypeService } from 'src/app/services/fuel-type.service';
import { GearTypeService } from 'src/app/services/gear-type.service';

@Component({
  selector: 'app-car-add-with-form',
  templateUrl: './car-add-with-form.component.html',
  styleUrls: ['./car-add-with-form.component.css'],
})
export class CarAddWithFormComponent implements OnInit {
  constructor(
    private carService: CarService,
    private colorService: ColorService,
    private brandService: BrandService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private fuelTypeService: FuelTypeService,
    private gearTypeService: GearTypeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createCarAddForm();
    this.getBrands();
    this.getColors();
    this.getGearTypes();
    this.getFuelTypes();
  }

  carAddForm!: FormGroup;

  addedCar!: Car;
  gearTypes: GearTypeViewDto[] = [];
  fuelTypes: FuelTypeViewDto[] = [];
  colors: ColorDto[] = [];
  brands: BrandDto[] = [];

  createCarAddForm() {
    this.carAddForm = this.formBuilder.group({
      brandName: ['', Validators.required],
      colorName: ['', Validators.required],
      fuelTypeName: ['', Validators.required],
      gearTypeName: ['', Validators.required],
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(255),
        ],
      ],
      horsePower: ['', [Validators.required, Validators.min(0)]],
      modelYear: ['', [Validators.required, Validators.min(1900)]],
      dailyPrice: ['', [Validators.required, Validators.min(0)]],
      description: ['', Validators.maxLength(500)],
      minCreditScore: [
        '',
        [Validators.required, Validators.min(0), Validators.max(1900)],
      ],
    });
  }

  get brandName() {
    return this.carAddForm.get('brandName');
  }
  get colorName() {
    return this.carAddForm.get('colorName');
  }
  get fuelTypeName() {
    return this.carAddForm.get('fuelTypeName');
  }
  get gearTypeName() {
    return this.carAddForm.get('gearTypeName');
  }
  get horsePower() {
    return this.carAddForm.get('horsePower');
  }
  get name() {
    return this.carAddForm.get('name');
  }
  get modelYear() {
    return this.carAddForm.get('modelYear');
  }
  get dailyPrice() {
    return this.carAddForm.get('dailyPrice');
  }
  get description() {
    return this.carAddForm.get('description');
  }
  get minCreditScore() {
    return this.carAddForm.get('minCreditScore');
  }

  getFuelTypes() {
    this.fuelTypeService.getAllViewDtos().subscribe(
      (response) => {
        this.fuelTypes = response.data;
      },
      (responseError) => {
        this.toastrService.error(ErrorHelper.getMessage(responseError));
      }
    );
  }

  getGearTypes() {
    this.gearTypeService.getAllViewDtos().subscribe(
      (response) => {
        this.gearTypes = response.data;
      },
      (responseError) => {
        this.toastrService.error(ErrorHelper.getMessage(responseError));
      }
    );
  }

  addCar() {
    if (this.carAddForm.valid) {
      let carAddDto: CarAddDto = this.carAddForm.value;
      this.carService.addCar(carAddDto).subscribe(
        (p) => {
          this.addedCar = p.data;
          this.toastrService.success(p.message);
          this.toastrService.success('Fotoğraf Ekleme sayfasına yönlendiriliyorsunuz.');
          timer(1000).subscribe((p) => {
            this.router.navigate(['/carimage/add/' + this.addedCar.id]);
          });
        },
        (error) => {
          this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
        }
      );
    } else {
      this.toastrService.warning('Validasyon Hatası');
    }
  }

  getColors() {
    this.colorService.getColors().subscribe((p) => {
      this.colors = p.data;
    });
  }

  getBrands() {
    this.brandService.getList().subscribe((p) => {
      this.brands = p.data;
    });
  }
}
