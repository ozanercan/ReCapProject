import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { PaymentAddDto } from 'src/app/models/Dtos/paymentAddDto';
import { CarService } from 'src/app/services/car.service';
import { PaymentService } from 'src/app/services/payment.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css'],
})
export class PaymentComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private paymentService: PaymentService,
    private carService: CarService
  ) {}

  rentalId!: string;
  nameSurname!: string;
  cardNumber!: string;
  expiryDate!: string;
  cvv!: string;
  price!: number;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['rentalId']) {
        this.rentalId = parameter['rentalId'];
        this.GetMoneyToPaidByRentalId(parameter['rentalId']);
      } else {
        this.toastrService.error(
          'Gerekli parametreler girilmeden ödeme yapılamaz!'
        );
      }
    });
  }

  GetMoneyToPaidByRentalId(rentalId: number) {
    this.carService.GetMoneyToPaidByRentalId(rentalId).subscribe(
      (p) => {
        this.price = p.data;
        this.toastrService.success(
          'Ödemeniz gereken ücret hesaplandı. Toplam: ' + p.data + ' ₺'
        );
      },
      (error) => {
        this.toastrService.error(error.error.message);
      }
    );
  }
  completePayment() {
    let paymentAddDto: PaymentAddDto = new PaymentAddDto();
    paymentAddDto.rentalId = this.rentalId;
    paymentAddDto.nameSurname = this.nameSurname;
    paymentAddDto.cardNumber = this.cardNumber;
    paymentAddDto.cvv = this.cvv;
    paymentAddDto.expiryDate = this.expiryDate;
    paymentAddDto.moneyPaid = this.price;

    this.paymentService.addPayment(paymentAddDto).subscribe(
      (p) => {
        this.toastrService.success(p.message);
        this.toastrService.success('Ana sayfaya yönlendiriliyorsunuz.');
        timer(3000).subscribe((p) => {
          window.location.href = '';
        });
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }
}
