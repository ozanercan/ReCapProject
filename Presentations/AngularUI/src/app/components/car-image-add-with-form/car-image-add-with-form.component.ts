import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CarImageService } from 'src/app/services/car-image.service';

@Component({
  selector: 'app-car-image-add-with-form',
  templateUrl: './car-image-add-with-form.component.html',
  styleUrls: ['./car-image-add-with-form.component.css'],
})
export class CarImageAddWithFormComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private carImageService: CarImageService,
    private toastrService: ToastrService
  ) {}

  carId!: number;

  carImageAddForm!: FormGroup;

  selectedFiles!: File[];

  imageUrls!: any[];

  createCarImageAddForm() {
    this.carImageAddForm = this.formBuilder.group({
      image: ['', [Validators.required]],
    });
  }

  get image() {
    return this.carImageAddForm.get('image');
  }

  ngOnInit(): void {
    this.createCarImageAddForm();
    this.activatedRoute.params.subscribe((p) => {
      if (p['carId']) {
        this.carId = parseInt(p['carId']);
      }
    });
  }

  onFileChanged(event: any) {
    this.selectedFiles = event.target.files;

    this.generateImageUrls();
  }

  private generateImageUrls() {
    this.imageUrls = [];

    for (let i = 0; i < this.selectedFiles.length; i++) {
      const selectedFile = this.selectedFiles[i];
      var reader = new FileReader();
      reader.readAsDataURL(selectedFile);
      reader.onload = (_event) => {
        this.imageUrls.push(_event.target?.result);
      };
    }
  }

  onUpload() {
    const formData = new FormData();
    for (let i = 0; i < this.selectedFiles.length; i++) {
      const element = this.selectedFiles[i];
      formData.append('formFiles', element, element.name);
    }

    this.carImageService.add(formData, this.carId).subscribe(
      (p) => {
        this.toastrService.success(p.message);

        timer(1000).subscribe((p) => {
          window.location.reload();
        });
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error));
      }
    );
  }
}
