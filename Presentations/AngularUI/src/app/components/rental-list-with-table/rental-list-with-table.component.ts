import { Component, OnInit } from '@angular/core';
import { RentalDto } from 'src/app/models/Dtos/rentalDto';
import { RentalService } from 'src/app/services/rental.service';
import { TitleService } from 'src/app/services/title.service';

@Component({
  selector: 'app-rental-list-with-table',
  templateUrl: './rental-list-with-table.component.html',
  styleUrls: ['./rental-list-with-table.component.css'],
})
export class RentalListWithTableComponent implements OnInit {
  constructor(
    private rentalService: RentalService,
    private titleService: TitleService
  ) {
    this.getRentals();
  }

  ngOnInit(): void {
    this.titleService.setTitle('Kiralamalar');
  }

  paidStatuMap: any = { false: 'Ödenmedi', true: 'Ödendi' };

  rentalDtos: RentalDto[] = [];

  getRentals() {
    this.rentalService.getRentals().subscribe((response) => {
      this.rentalDtos = response.data;
    });
  }

  getRentalPaidStatuClass(rentalDto: RentalDto): string {
    if (rentalDto.isPaid == true) {
      return 'badge bg-success';
    } else {
      return 'badge bg-danger';
    }
  }
}
