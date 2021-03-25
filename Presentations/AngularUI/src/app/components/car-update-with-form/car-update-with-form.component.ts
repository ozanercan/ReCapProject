import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
        this.getCarById(parameter['carId']);
      } else {
        this.toastrService.error('Parametreler eksik.');
      }
    });

    this.getColors();
    this.getBrands();
  }

  car!: CarDto;

  carUpdateForm!: FormGroup;

  colors: ColorDto[] = [];
  brands: BrandDto[] = [];

  selectedBrand!: string;
  selectedColor!: string;

  getCarById(id: number) {
    this.carService.getById(id).subscribe(
      (p) => {
        this.car = p.data;
        this.setId();
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
      modelYear: ['', Validators.required],
      dailyPrice: ['', Validators.required],
      description: ['', Validators.maxLength(500)],
    });
  }

  updateCar() {
    if (this.carUpdateForm.valid) {
      let carUpdateDto: CarUpdateDto = this.carUpdateForm.value;
      carUpdateDto.id = this.car.id;

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
    this.colorService.getColors().subscribe((p) => {
      this.colors = p.data;
      this.setInitColor();
    });
  }

  getBrands() {
    this.brandService.getList().subscribe((p) => {
      this.brands = p.data;
      this.setInitBrand();
    });
  }

  setInitColor() {
    this.colors.forEach((p) => {
      if (p.id === this.car.colorId) {
        this.selectedColor = p.name;
        this.carUpdateForm.get('colorName')?.setValue(this.selectedColor);
      }
    });
  }
  setInitBrand() {
    this.brands.forEach((p) => {
      if (p.id === this.car.brandId) {
        this.selectedBrand = p.name;
        this.carUpdateForm.get('brandName')?.setValue(this.selectedBrand);
      }
    });
  }
  setId() {
    this.carUpdateForm.get('id')?.setValue(this.car.id);
  }
}
