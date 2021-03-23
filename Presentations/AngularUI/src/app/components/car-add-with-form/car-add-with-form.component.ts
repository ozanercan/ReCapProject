import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { Brand } from 'src/app/models/brand';
import { CarAddDto } from 'src/app/models/carAddDto';
import { Color } from 'src/app/models/color';
import { BrandService } from 'src/app/services/brand.service';
import { CarService } from 'src/app/services/car.service';
import { ColorService } from 'src/app/services/color.service';

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
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createCarAddForm();
    this.getBrands();
    this.getColors();
  }

  carAddForm!: FormGroup;

  colors: Color[] = [];
  brands: Brand[] = [];

  selectedBrand!: string;
  selectedColor!: string;

  createCarAddForm() {
    this.carAddForm = this.formBuilder.group({
      brandName: ['', Validators.required],
      colorName: ['', Validators.required],
      modelYear: ['', Validators.required],
      dailyPrice: ['', Validators.required],
      description: ['', Validators.maxLength(500)],
    });
  }

  addCar() {
    if (this.carAddForm.valid) {
      let carAddDto: CarAddDto = this.carAddForm.value;

      this.carService.addCar(carAddDto).subscribe(
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
    });
  }

  getBrands() {
    this.brandService.getList().subscribe((p) => {
      this.brands = p.data;
    });
  }
}
