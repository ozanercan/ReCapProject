import { Component, OnInit } from '@angular/core';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
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

  brands: BrandDto[] = [];

  filterText: string = '';
  getBrands() {
    this.brandService.getList().subscribe((response) => {
      this.brands = response.data;
    });
  }
}
