import { Component, OnInit } from '@angular/core';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
import { BrandService } from 'src/app/services/brand.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-brand-list-with-table',
  templateUrl: './brand-list-with-table.component.html',
  styleUrls: ['./brand-list-with-table.component.css'],
})
export class BrandListWithTableComponent implements OnInit {
  constructor(
    private brandService: BrandService,
    private titleService: TitleService
  ) {
    this.getBrands();
  }

  ngOnInit(): void {
    this.titleService.setTitle('Markalar');
  }

  brands: BrandDto[] = [];

  filterText: string = '';
  getBrands() {
    this.brandService.getList().subscribe((response) => {
      this.brands = response.data;
    });
  }
}
