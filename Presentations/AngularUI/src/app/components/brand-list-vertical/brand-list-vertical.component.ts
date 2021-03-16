import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Brand } from 'src/app/models/brand';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-list-vertical',
  templateUrl: './brand-list-vertical.component.html',
  styleUrls: ['./brand-list-vertical.component.css'],
})
export class BrandListVerticalComponent implements OnInit {
  constructor(private brandService: BrandService) {
    this.getBrands();
  }

  ngOnInit(): void {}

  brands: Brand[] = [];

  currentBrand!: Brand;

  @Output() outCurrentBrand = new EventEmitter<Brand>();

  getBrands() {
    this.brandService.getBrands().subscribe((response) => {
      this.brands = response.data;
    });
  }

  setCurrentBrand(brand: Brand) {
    this.currentBrand = brand;

    this.outCurrentBrand.emit(brand);
  }
}
