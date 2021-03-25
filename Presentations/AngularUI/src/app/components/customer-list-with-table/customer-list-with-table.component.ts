import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CustomerDetailDto } from 'src/app/models/Dtos/customerDetailDto';
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
  
  selectedCustomerDetailDto!: CustomerDetailDto;

  @Output()
  selectedCustomer: EventEmitter<CustomerDetailDto> = new EventEmitter();

  getCustomerDetails() {
    this.customerService.getCustomerDetailDtos().subscribe((response) => {
      this.customerDetails = response.data;
    });
  }

  setCustomer(customerDetailDto: CustomerDetailDto) {
    this.selectedCustomerDetailDto = customerDetailDto;
    this.selectedCustomer.emit(this.selectedCustomerDetailDto);
  }


  getTableRowClass(customerDetailDto: CustomerDetailDto): string {
    if (this.selectedCustomerDetailDto !== undefined) {
      if (this.selectedCustomerDetailDto.id == customerDetailDto.id) {
        return 'table-success';
      }
    }
    return '';
  }
}
