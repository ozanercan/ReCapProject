import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { ColorDto } from 'src/app/models/Dtos/colorDto';
import { ColorUpdateDto } from 'src/app/models/Dtos/colorUpdateDto';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-color-update-with-form',
  templateUrl: './color-update-with-form.component.html',
  styleUrls: ['./color-update-with-form.component.css'],
})
export class ColorUpdateWithFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private colorService: ColorService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createColorUpdateForm();
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['colorId']) {
        this.getColorById(parameter['colorId']);
      } else {
        this.toastrService.error('Parametreler eksik.');
      }
    });
  }

  color!: ColorDto;

  colorUpdateForm!: FormGroup;

  getColorById(id: number) {
    this.colorService.getById(id).subscribe(
      (p) => {
        this.color = p.data;
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  createColorUpdateForm() {
    this.colorUpdateForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  updateColor() {
    if (this.colorUpdateForm.valid) {
      let colorUpdateDto: ColorUpdateDto = this.colorUpdateForm.value;

      colorUpdateDto.id = this.color.id;

      this.colorService.update(colorUpdateDto).subscribe(
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
