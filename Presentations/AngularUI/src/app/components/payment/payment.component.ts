import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { PaymentAddDto } from 'src/app/models/paymentAddDto';
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
    private paymentService: PaymentService
  ) {}

  rentalId!: string;
  nameSurname!: string;
  cardNumber!: string;
  expiryDate!: string;
  cvv!: string;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['rentalId']) {
        this.rentalId = parameter['rentalId'];
      } else {
        this.toastrService.error(
          'Gerekli parametreler girilmeden ödeme yapılamaz!'
        );
      }
    });
  }
  completePayment() {
    let paymentAddDto: PaymentAddDto = new PaymentAddDto();
    paymentAddDto.rentId = this.rentalId;
    paymentAddDto.nameSurname = this.nameSurname;
    paymentAddDto.cardNumber = this.cardNumber;
    paymentAddDto.cvv = this.cvv;
    paymentAddDto.expiryDate = this.expiryDate;

    
    console.log(paymentAddDto);

    this.paymentService.addPayment(paymentAddDto).subscribe(
      (p) => {
        this.toastrService.success(p.message);
        this.toastrService.success("Ana sayfaya yönlendiriliyorsunuz.");
        timer(3000).subscribe(p=>{
          window.location.href="";
        });
      },
      (error) => {
        this.toastrService.error(error.error.message);
      }
    );
  }
}
