import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { BrandAddDto } from 'src/app/models/brandAddDto';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-add-with-form',
  templateUrl: './brand-add-with-form.component.html',
  styleUrls: ['./brand-add-with-form.component.css'],
})
export class BrandAddWithFormComponent implements OnInit {
  constructor(
    private brandService: BrandService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {}

  brandName!: string;

  addBrand() {
    let brandAddDto = new BrandAddDto();
    brandAddDto.name = this.brandName;

    this.brandService.addBrand(brandAddDto).subscribe(
      (p) => {
        this.toastrService.success(p.message);
      },
      (error:HttpErrorResponse) => {
       this.toastrService.error(ErrorHelper.getMessage(error));
      }
    );
  }
}
