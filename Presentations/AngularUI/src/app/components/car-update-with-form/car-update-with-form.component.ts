import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
import { CarDto } from 'src/app/models/Dtos/carDto';
import { CarUpdateDto } from 'src/app/models/Dtos/carUpdateDto';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
import { BrandService } from 'src/app/services/brand.service';
import { CarService } from 'src/app/services/car.service';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-car-update-with-form',
  templateUrl: './car-update-with-form.component.html',
  styleUrls: ['./car-update-with-form.component.css'],
})
export class CarUpdateWithFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private carService: CarService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private colorService: ColorService,
    private brandService: BrandService
  ) {}

  ngOnInit(): void {
    this.createCarUpdateForm();

    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['carId']) {
        this.getCarUpdateDtoById(parameter['carId']);
      } else {
        this.toastrService.error('Parametreler eksik.');
      }
    });
  }

  carUpdateDto!: CarUpdateDto;

  carUpdateForm!: FormGroup;

  colors: ColorDto[] = [];
  brands: BrandDto[] = [];

  selectedBrand!: string;
  selectedColor!: string;

  getCarUpdateDtoById(id: number) {
    this.carService.getCarUpdateDtoByCarId(id).subscribe(
      (p) => {
        this.carUpdateDto = p.data;
        this.setId();
        this.getColors();
        this.getBrands();
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  createCarUpdateForm() {
    this.carUpdateForm = this.formBuilder.group({
      id: ['', Validators.required],
      brandName: ['', Validators.required],
      colorName: ['', Validators.required],
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
    return this.carUpdateForm.get('brandName');
  }
  get colorName() {
    return this.carUpdateForm.get('colorName');
  }
  get modelYear() {
    return this.carUpdateForm.get('modelYear');
  }
  get dailyPrice() {
    return this.carUpdateForm.get('dailyPrice');
  }
  get description() {
    return this.carUpdateForm.get('description');
  }
  get minCreditScore() {
    return this.carUpdateForm.get('minCreditScore');
  }

  updateCar() {
    if (this.carUpdateForm.valid) {
      let carUpdateDto: CarUpdateDto = this.carUpdateForm.value;
      carUpdateDto.id = this.carUpdateDto.id;

      this.carService.update(carUpdateDto).subscribe(
        (p) => {
          this.toastrService.success(p.message);
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
    this.colorService.getColors().subscribe(
      (p) => {
        this.colors = p.data;
        this.setInitColor();
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  getBrands() {
    this.brandService.getList().subscribe(
      (p) => {
        this.brands = p.data;
        this.setInitBrand();
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  setInitColor() {
    this.colors.forEach((p) => {
      if (p.name === this.carUpdateDto.colorName) {
        this.selectedColor = p.name;
        this.colorName?.setValue(this.selectedColor);
      }
    });
  }
  setInitBrand() {
    this.brands.forEach((p) => {
      if (p.name === this.carUpdateDto.brandName) {
        this.selectedBrand = p.name;
        this.brandName?.setValue(this.selectedBrand);
      }
    });
  }
  setId() {
    this.carUpdateForm.get('id')?.setValue(this.carUpdateDto.id);
  }
}
