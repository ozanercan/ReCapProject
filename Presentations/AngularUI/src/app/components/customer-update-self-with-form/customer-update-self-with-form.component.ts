import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { CustomerDetailDto } from 'src/app/models/Dtos/customerDetailDto';
import { CustomerUpdateWithUserDto } from 'src/app/models/Dtos/customerUpdateWithUserDto';
import { CustomerService } from 'src/app/services/customer.service';
import { RememberMeService } from 'src/app/services/remember-me.service';

@Component({
  selector: 'app-customer-update-self-with-form',
  templateUrl: './customer-update-self-with-form.component.html',
  styleUrls: ['./customer-update-self-with-form.component.css'],
})
export class CustomerUpdateSelfWithFormComponent implements OnInit {
  constructor(
    private toastrService: ToastrService,
    private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private rememberMeService:RememberMeService
  ) {}

  ngOnInit(): void {
    this.createUpdateForm();
    this.getActiveDatas();
  }
  updateForm!: FormGroup; 
  customerDetails!:CustomerDetailDto;

  getActiveDatas(){
    let emailInCookie = this.rememberMeService.getEmail()!;
    this.customerService.getCustomerDetailByEmail(emailInCookie).subscribe(response=>{
      this.customerDetails = response.data;
    }, errorResponse=>{
      this.toastrService.error(ErrorHelper.getMessage(errorResponse));
    });
  }

  createUpdateForm() {
    this.updateForm = this.formBuilder.group({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      companyName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      activePassword: new FormControl('', Validators.required),
      newPassword: new FormControl(''),
    });
  }

  update() {
    if (this.updateForm.valid) {
      let customerUpdateWithUserDto : CustomerUpdateWithUserDto = this.updateForm.value;
      customerUpdateWithUserDto.id = this.customerDetails.id;
      this.customerService.updateWithUser(customerUpdateWithUserDto).subscribe(response=>{
        this.toastrService.success(response.message);
      }, error=>{
        this.toastrService.error(ErrorHelper.getMessage(error));
      });
    } else {
      this.toastrService.warning(
        'Lütfen tüm alanları istenildiği şekilde doldurunuz.'
      );
    }
  }
}
