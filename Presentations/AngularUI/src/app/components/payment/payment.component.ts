import { createDirectiveTypeParams } from '@angular/compiler/src/render3/view/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CustomerCreditCardAddDto } from 'src/app/models/Dtos/customerCreditCardAddDto';
import { CustomerCreditCardDto } from 'src/app/models/Dtos/customerCreditCardDto';
import { PaymentAddDto } from 'src/app/models/Dtos/paymentAddDto';
import { CarService } from 'src/app/services/car.service';
import { CustomerCreditCardService } from 'src/app/services/customer-credit-card.service';
import { PaymentService } from 'src/app/services/payment.service';
import { RentalService } from 'src/app/services/rental.service';

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
    private carService: CarService,
    private rentalService: RentalService,
    private customerCreditCardService: CustomerCreditCardService
  ) {}

  rentalId!: string;
  nameSurname!: string;
  cardNumber!: string;
  expiryDate!: string;
  cvv!: string;
  price!: number;
  customerId!: number;
  creditCards!: CustomerCreditCardDto[];

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['rentalId']) {
        this.rentalId = parameter['rentalId'];
        this.GetMoneyToPaidByRentalId(parameter['rentalId']);
        timer(500).subscribe(
          p=>{
            this.getCustomerIdByRentalId(parameter['rentalId']);
          }
        );
        timer(1000).subscribe(
          p=>{
            this.getCreditCards();  
          }
        );
        
      } else {
        this.toastrService.error(
          'Gerekli parametreler girilmeden ödeme yapılamaz!'
        );
      }
    });
  }
  setCurrentCreditCart(creditCart:CustomerCreditCardDto){
    this.nameSurname = creditCart.cardOwnerFullName;
    this.cardNumber = creditCart.cardNumber;
    this.expiryDate = creditCart.expiryDate;
    this.cvv = creditCart.cvv;
  }

  getCreditCards() {
    this.customerCreditCardService
      .getCardsByCustomerId(this.customerId)
      .subscribe(
        (response) => {
          this.creditCards = response.data;
        },
        (error) => {
          this.toastrService.error(ErrorHelper.getMessage(error));
        }
      );
  }

  getCustomerIdByRentalId(rentalId: number) {
    this.rentalService.getCustomerIdById(rentalId).subscribe(
      (response) => {
        this.customerId = response.data;
      },
      (error) => {
        
        this.toastrService.error(ErrorHelper.getMessage(error));
      }
    );
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
    let paymentAddDto: PaymentAddDto = {
      moneyPaid: this.price,
      rentalId: this.rentalId,
    };
    this.paymentService.addPayment(paymentAddDto).subscribe(
      (p) => {
        this.toastrService.success(p.message);
        // this.toastrService.success('Ana sayfaya yönlendiriliyorsunuz.');
        // timer(3000).subscribe((p) => {
        //   window.location.href = '';
        // });
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
      }
    );
  }

  addCreditCard() {
    let customerCreditCardAddDto: CustomerCreditCardAddDto = {
      cardNumber: this.cardNumber,
      cvv: this.cvv,
      expiryDate: this.expiryDate,
      cardOwnerFullName: this.nameSurname,
      userId: this.customerId,
    };
    this.customerCreditCardService.add(customerCreditCardAddDto).subscribe(
      (response) => {
        this.toastrService.success(response.message);
      },
      (error) => {
        this.toastrService.error(ErrorHelper.getMessage(error));
      }
    );
  }
}
