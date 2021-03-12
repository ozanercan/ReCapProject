import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models/customer/customer';
import { CustomerService } from 'src/app/services/customer/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css'],
})
export class CustomerListComponent implements OnInit {
  constructor(private customerService: CustomerService) {
    this.getCustomers();
  }

  ngOnInit(): void {}

  customers: Customer[] = [];

  getCustomers() {
    this.customerService.getCustomers().subscribe((response) => {
      this.customers = response.data;
    });
  }
}
