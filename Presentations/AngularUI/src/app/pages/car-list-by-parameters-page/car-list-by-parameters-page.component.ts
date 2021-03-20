import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Brand } from 'src/app/models/brand';
import { Color } from 'src/app/models/color';
import { BrandService } from 'src/app/services/brand.service';
import { ColorService } from 'src/app/services/color.service';

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
    private router: Router
  ) {
    this.getColors();
    this.getBrands();
  }

  colors: Color[] = [];
  brands: Brand[] = [];

  selectedBrand!: string;
  selectedColor!: string;

  ngOnInit(): void {}

  getColors() {
    this.colorService.getColors().subscribe((p) => {
      this.colors = p.data;
    });
  }

  getBrands() {
    this.brandService.getBrands().subscribe((p) => {
      this.brands = p.data;
    });
  }

  filter() {
    if (
      this.selectedBrand !== undefined &&
      this.selectedBrand.toLocaleLowerCase().indexOf('seçiniz') == -1 &&
      this.selectedColor !== undefined &&
      this.selectedColor.toLocaleLowerCase().indexOf('seçiniz') == -1
    ) {
      let routePath =
        'carListByParameters/carListWithCard/filters/' +
        this.selectedColor +
        '/' +
        this.selectedBrand;

      this.router.navigateByUrl(routePath);
    } else {
      this.toastrService.warning('Lütfen Renk ve Marka seçimi yapınız.');
    }
  }
}
