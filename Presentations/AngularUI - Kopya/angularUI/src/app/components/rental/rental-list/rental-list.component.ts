import { Component, OnInit } from '@angular/core';
import { RentalDto } from 'src/app/models/rental/rentalDto';
import { RentalService } from 'src/app/services/rental/rental.service';

@Component({
  selector: 'app-rental-list',
  templateUrl: './rental-list.component.html',
  styleUrls: ['./rental-list.component.css']
})
export class RentalListComponent implements OnInit {
  constructor(private rentalService: RentalService) {
    this.getRentals();
  }

  rentalDtos: RentalDto[] = [];
  ngOnInit(): void {}

  getRentals() {
    this.rentalService.getRentalDtos().subscribe((response) => {
      this.rentalDtos = response.data;
    });
  }
}
