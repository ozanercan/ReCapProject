import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';
import { CustomerDetailDto } from 'src/app/models/Dtos/customerDetailDto';
import { RentalCreateDto } from 'src/app/models/Dtos/rentalCreateDto';
import { CarService } from 'src/app/services/car.service';
import { CustomerService } from 'src/app/services/customer.service';
import { RentalService } from 'src/app/services/rental.service';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-rental-new-page',
  templateUrl: './rental-new-page.component.html',
  styleUrls: ['./rental-new-page.component.css'],
})
export class RentalNewPageComponent implements OnInit {
  constructor(
    private carService: CarService,
    private customerService: CustomerService,
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private rentalService: RentalService,
    private router: Router,
    private authService: AuthService,
    private rememberMeService: RememberMeService,
    private titleService: TitleService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['carId']) {
        this.carId = parseInt(parameter['carId']);
        this.getCarDetailDtoFromService(parameter['carId']);
      }
    });
  }

  carId!: number;

  dateTimeNow: string = new Date().toISOString();

  carDetailDto!: CarDetailDto;

  rentDate!: Date;

  returnDate!: Date;

  totalPrice!: number;
  getCarTotalPriceCalculate() {
    if (this.rentDate != undefined && this.returnDate != undefined) {
      this.carService
        .getCalculateTotalPrice({
          carId: this.carId,
          rentDateTime: this.rentDate,
          returnDateTime: this.returnDate,
        })
        .subscribe(
          (response) => {
            this.totalPrice = response.data;
            this.toastrService.success(
              `Fiyat Hesaplandı: <b>${response.data} ₺</b>`
            );
          },
          (responseError) => {
            this.toastrService.error(ErrorHelper.getMessage(responseError));
          }
        );
    }
  }

  getCarDetailDtoFromService(carId: number) {
    this.carService.getCarDetailByCarId(carId).subscribe((p) => {
      this.carDetailDto = p.data;
    });
  }

  getCarDetailDto(): CarDetailDto {
    return this.carDetailDto;
  }

  getValidateStatusForButton(): boolean {
    if (this.rentDate == undefined || this.returnDate == undefined) {
      return true;
    }

    return false;
  }

  create() {
    if (this.authService.isAuthentication()) {
      if (this.rentDate === undefined) {
        this.toastrService.warning('Lütfen Kira Başlangıç Tarihini seçin.');
      } else if (this.returnDate === undefined) {
        this.toastrService.warning('Lütfen Kira Bitiş Tarihini seçin.');
      } else {
        let rentalCreateDto: RentalCreateDto = new RentalCreateDto();
        rentalCreateDto.carId = this.carDetailDto.id;
        rentalCreateDto.customerId = this.rememberMeService.getUser().id;
        rentalCreateDto.rentDate = this.rentDate;

        rentalCreateDto.returnDate = this.returnDate;
        this.rentalService.addRental(rentalCreateDto).subscribe(
          async (p) => {
            this.toastrService.success(
              'Kayıt oluşturuldu, ödeme sistemine yönlendiriliyorsunuz.'
            );

            const numbers = timer(3000);

            numbers.subscribe((x) =>
              this.router.navigate(['payment/add/' + p.data.id])
            );
          },
          (error) => {
            this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
          }
        );
      }
    } else {
      this.toastrService.warning('Lütfen önce giriş yapınız.');
      document.getElementById('showLoginModal')?.click();
    }
  }
}
