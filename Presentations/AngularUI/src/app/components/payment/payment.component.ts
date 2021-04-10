import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { TitleService } from 'src/app/services/title.service';

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
    private customerCreditCardService: CustomerCreditCardService,
    private formBuilder: FormBuilder,
    private titleService: TitleService
  ) {}

  // Required
  rentalId!: string;
  customerId!: number;
  isCanPayment: boolean = false;

  // For NgModel
  nameSurname!: string;
  cardNumber!: string;
  expiryDate!: string;
  cvv!: string;
  price!: number;

  // Model For Form Element Access
  get form_cardOwnerFullName() {
    return this.paymentForm.get('cardOwnerFullName');
  }
  get form_cvv() {
    return this.paymentForm.get('cvv');
  }
  get form_expiryDate() {
    return this.paymentForm.get('expiryDate');
  }
  get form_cardNumber() {
    return this.paymentForm.get('cardNumber');
  }

  creditCards!: CustomerCreditCardDto[];
  paymentForm!: FormGroup;

  ngOnInit(): void {
    this.titleService.setTitle('Ödeme Yap');

    this.createPaymentForm();

    this.activatedRoute.params.subscribe((parameter) => {
      if (parameter['rentalId']) {
        this.rentalId = parameter['rentalId'];
        this.GetMoneyToPaidByRentalId(parameter['rentalId']);
        timer(500).subscribe((p) => {
          this.getCustomerIdByRentalId(parameter['rentalId']);
        });
        timer(1000).subscribe((p) => {
          this.getCreditCards();
        });
        timer(1500).subscribe((p) => {
          this.getIsCanPayment();
        });
      } else {
        this.toastrService.error(
          'Gerekli parametreler girilmeden ödeme yapılamaz!'
        );
      }
    });
  }

  // ngOnInit(): void {
  //   this.createPaymentForm();

  //   this.activatedRoute.params.subscribe((parameter) => {
  //     if (parameter['rentalId']) {
  //       this.rentalId = parameter['rentalId'];
  //       this.GetMoneyToPaidByRentalId(parameter['rentalId']);
  //       timer(500).subscribe((p) => {
  //         this.getCustomerIdByRentalId(parameter['rentalId']);
  //       });
  //       timer(1000).subscribe((p) => {
  //         this.getCreditCards();
  //       });
  //     } else {
  //       this.toastrService.error(
  //         'Gerekli parametreler girilmeden ödeme yapılamaz!'
  //       );
  //     }
  //   });
  // }

  createPaymentForm() {
    this.paymentForm = this.formBuilder.group({
      cardOwnerFullName: [
        '',
        [
          Validators.required,
          Validators.maxLength(50),
          Validators.minLength(3),
        ],
      ],
      cardNumber: [
        '',
        [
          Validators.required,
          Validators.maxLength(20),
          Validators.minLength(10),
        ],
      ],
      expiryDate: ['', [Validators.required, Validators.maxLength(5)]],
      cvv: ['', [Validators.required, Validators.maxLength(3)]],
    });
  }
  getIsCanPayment() {
    this.paymentService.getIsCanPayment(this.rentalId).subscribe(
      (response) => {
        this.isCanPayment = true;
      },
      (responseError) => {
        this.toastrService.error(ErrorHelper.getMessage(responseError));
      }
    );
  }
  creditCartExist(cardNumber: string): boolean {
    let isHaveCard = false;
    if (this.creditCards === undefined) return false;

    this.creditCards.forEach((p) => {
      if (cardNumber === p.cardNumber) {
        isHaveCard = true;
      }
    });
    return isHaveCard;
  }

  setCurrentCreditCart(creditCart: CustomerCreditCardDto) {
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

  askForCardRegister() {
    if (!this.creditCartExist(this.cardNumber)) {
      document.getElementById('creditCartSaveModalTrigger')?.click();
    }
  }

  completePayment() {
    if (this.paymentForm.valid) {
      let paymentAddDto: PaymentAddDto = {
        moneyPaid: this.price,
        rentalId: this.rentalId,
      };
      this.paymentService.addPayment(paymentAddDto).subscribe(
        (p) => {

          this.isCanPayment = false;
          this.toastrService.success(p.message);

          this.askForCardRegister();
        },
        (error) => {
          this.toastrService.error(ErrorHelper.getMessage(error), 'HATA');
        }
      );
    } else {
      this.toastrService.warning(
        'Lütfen Kart bilgilerinizi eksiksiz doldurun.'
      );
    }
  }

  addCreditCard() {
    if (this.paymentForm.valid) {
      let customerCreditCardAddDto: CustomerCreditCardAddDto = this.paymentForm
        .value;
      customerCreditCardAddDto.userId = this.customerId;

      this.customerCreditCardService.add(customerCreditCardAddDto).subscribe(
        (response) => {
          this.toastrService.success(response.message);
        },
        (error) => {
          this.toastrService.error(ErrorHelper.getMessage(error));
        }
      );
    } else {
      this.toastrService.warning(
        'Lütfen Kart bilgilerini eksiksiz şekilde doldurun.'
      );
    }
  }
}
