import { Component, OnInit, ɵɵtrustConstantResourceUrl } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { ColorAddDto } from 'src/app/models/Dtos/colorAddDto';
import { BrandService } from 'src/app/services/brand.service';
import { ColorService } from 'src/app/services/color.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-color-add-with-form',
  templateUrl: './color-add-with-form.component.html',
  styleUrls: ['./color-add-with-form.component.css'],
})
export class ColorAddWithFormComponent implements OnInit {
  constructor(
    private colorService: ColorService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private titleService: TitleService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('Yeni Renk Oluştur');
    this.createColorAddForm();
  }

  colorAddForm!: FormGroup;

  createColorAddForm() {
    this.colorAddForm = this.formBuilder.group({
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

  get colorName() {
    return this.colorAddForm.get('name');
  }

  addColor() {
    if (this.colorAddForm.valid) {
      let colorAddDto: ColorAddDto = this.colorAddForm.value;

      this.colorService.addColor(colorAddDto).subscribe(
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
