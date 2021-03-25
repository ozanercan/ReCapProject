import { Component, OnInit } from '@angular/core';
import { RentalDto } from 'src/app/models/Dtos/rentalDto';
import { RentalService } from 'src/app/services/rental.service';

@Component({
  selector: 'app-rental-list-with-table',
  templateUrl: './rental-list-with-table.component.html',
  styleUrls: ['./rental-list-with-table.component.css'],
})
export class RentalListWithTableComponent implements OnInit {
  constructor(private rentalService: RentalService) {
    this.getRentals();
  }

  ngOnInit(): void {}
  rentalDtos: RentalDto[] = [];
  getRentals() {
    this.rentalService.getRentals().subscribe((response) => {
      this.rentalDtos = response.data;
    });
  }
}
