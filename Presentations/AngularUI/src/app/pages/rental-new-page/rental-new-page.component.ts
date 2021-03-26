import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarDetailDto } from 'src/app/models/Dtos/carDetailDto';
import { CustomerDetailDto } from 'src/app/models/Dtos/customerDetailDto';
import { RentalCreateDto } from 'src/app/models/Dtos/rentalCreateDto';
import { CarService } from 'src/app/services/car.service';
import { CustomerService } from 'src/app/services/customer.service';
import { RentalService } from 'src/app/services/rental.service';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';

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
    private rentalService: RentalService
  ) {}

  ngOnInit(): void {
    
    let nowDate = new Date().toLocaleString()

    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['carId']) {
        this.getCarDetailDtoFromService(parameter['carId']);
      } else {
        this.toastrService.error('Girilen parametre yanlış.');
      }
    });
  }

  dateTimeNow : string = new Date().toISOString();

  carDetailDto!: CarDetailDto;

  customerDetailDto!: CustomerDetailDto;

  rentDate!: Date;

  returnDate!: Date;

  getCarDetailDtoFromService(carId: number) {
    this.carService.getCarDetailByCarId(carId).subscribe((p) => {
      this.carDetailDto = p.data;
    });
  }

  getCarDetailDto(): CarDetailDto {
    return this.carDetailDto;
  }

  setCustomerDetailDto(customerDetailDto: CustomerDetailDto) {
    this.customerDetailDto = customerDetailDto;
  }

  create() {
    console.log('create run')
    console.log(this.customerDetailDto)
    if (this.customerDetailDto === undefined) {
      this.toastrService.warning(
        'Lütfen otomobili kiralayacağınız müşteriyi seçin.'
      );
    } else if (this.rentDate === undefined) {
      this.toastrService.warning('Lütfen Kira Başlangıç Tarihini seçin.');
    } else if (this.returnDate === undefined) {
      this.toastrService.warning('Lütfen Kira Bitiş Tarihini seçin.');
    } else {
      let rentalCreateDto: RentalCreateDto = new RentalCreateDto();
      rentalCreateDto.carId = this.carDetailDto.id;
      rentalCreateDto.customerId = this.customerDetailDto.id;
      rentalCreateDto.rentDate = this.rentDate;

      rentalCreateDto.returnDate = this.returnDate;
      this.rentalService.addRental(rentalCreateDto).subscribe(
        async (p) => {
          this.toastrService.success(
            'Kayıt oluşturuldu, ödeme sistemine yönlendiriliyorsunuz.'
          );

          const numbers = timer(3000);

          numbers.subscribe(
            (x) => (window.location.href = 'payment/' + p.data.id)
          );
        },
        (error) => {
          this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
        }
      );
    }
  }
}
