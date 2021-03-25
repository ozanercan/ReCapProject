import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
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

  brands: BrandDto[] = [];

  currentBrand!: BrandDto;

  @Output() outCurrentBrand = new EventEmitter<BrandDto>();

  getBrands() {
    this.brandService.getList().subscribe((response) => {
      this.brands = response.data;
    });
  }

  setCurrentBrand(brand: BrandDto) {
    this.currentBrand = brand;

    this.outCurrentBrand.emit(brand);
  }
}
