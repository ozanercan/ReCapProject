import { Component, OnInit } from '@angular/core';
import { CustomerDetailDto } from 'src/app/models/customerDetailDto';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer-list-with-table',
  templateUrl: './customer-list-with-table.component.html',
  styleUrls: ['./customer-list-with-table.component.css'],
})
export class CustomerListWithTableComponent implements OnInit {
  constructor(private customerService: CustomerService) {
    this.getCustomerDetails();
  }

  ngOnInit(): void {}

  customerDetails: CustomerDetailDto[] = [];
  
  getCustomerDetails() {
    this.customerService.getColors().subscribe((response) => {

      this.customerDetails = response.data;
    });
  }
}
