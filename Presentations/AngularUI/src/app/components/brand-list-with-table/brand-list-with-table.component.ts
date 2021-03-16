import { Component, OnInit } from '@angular/core';
import { Brand } from 'src/app/models/brand';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-list-with-table',
  templateUrl: './brand-list-with-table.component.html',
  styleUrls: ['./brand-list-with-table.component.css'],
})
export class BrandListWithTableComponent implements OnInit {
  constructor(private brandService: BrandService) {
    this.getBrands();
  }

  ngOnInit(): void {}

  brands: Brand[] = [];

  getBrands() {
    this.brandService.getBrands().subscribe((response) => {
      this.brands = response.data;
    });
  }
}
