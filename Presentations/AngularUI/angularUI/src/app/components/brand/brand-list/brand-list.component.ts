import { Component, OnInit } from '@angular/core';
import { Brand } from 'src/app/models/brand/brand';
import { BrandService } from 'src/app/services/brand/brand.service';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css']
})
export class BrandListComponent implements OnInit {

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
