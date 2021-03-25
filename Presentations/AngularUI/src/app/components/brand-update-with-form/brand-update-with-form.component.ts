import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { BrandDto } from 'src/app/models/Dtos/brandDto';
import { BrandUpdateDto } from 'src/app/models/Dtos/brandUpdateDto';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-update-with-form',
  templateUrl: './brand-update-with-form.component.html',
  styleUrls: ['./brand-update-with-form.component.css'],
})
export class BrandUpdateWithFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private brandService: BrandService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createBrandUpdateForm();
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['brandId']) {
        this.getBrandById(parameter['brandId']);
      } else {
        this.toastrService.error('Parametreler eksik.');
      }
    });
  }

  brand!: BrandDto;

  getBrandById(id: number) {
    this.brandService.getById(id).subscribe(
      (p) => {
        this.brand = p.data;
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  brandUpdateForm!: FormGroup;

  createBrandUpdateForm() {
    this.brandUpdateForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  updateBrand() {
    if (this.brandUpdateForm.valid) {
      let brandUpdateDto: BrandUpdateDto = this.brandUpdateForm.value;
      brandUpdateDto.id = this.brand.id;

      this.brandService.update(brandUpdateDto).subscribe(
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
}
