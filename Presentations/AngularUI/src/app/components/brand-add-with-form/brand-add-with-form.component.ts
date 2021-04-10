import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { BrandAddDto } from 'src/app/models/Dtos/brandAddDto';
import { BrandService } from 'src/app/services/brand.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-brand-add-with-form',
  templateUrl: './brand-add-with-form.component.html',
  styleUrls: ['./brand-add-with-form.component.css'],
})
export class BrandAddWithFormComponent implements OnInit {
  constructor(
    private brandService: BrandService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private titleService: TitleService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('Yeni Marka Oluştur');
    this.createBrandAddForm();
  }

  brandAddForm!: FormGroup;

  get name() {
    return this.brandAddForm.get('name');
  }

  createBrandAddForm() {
    this.brandAddForm = this.formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.maxLength(50),
          Validators.minLength(2),
        ],
      ],
    });
  }

  addBrand() {
    console.log(this.name);
    if (this.brandAddForm.valid) {
      let brandAddDto: BrandAddDto = this.brandAddForm.value;

      this.brandService.add(brandAddDto).subscribe(
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
