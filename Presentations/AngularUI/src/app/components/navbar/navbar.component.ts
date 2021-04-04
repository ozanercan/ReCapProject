import { Component, OnInit } from '@angular/core';
import { DropDownNav } from 'src/app/models/navbar/dropDownNav';
import { Nav } from 'src/app/models/navbar/nav';
import { NavbarService } from 'src/app/services/navbar.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private navbarService: NavbarService) {}
  ngOnInit(): void {
    this.getNavText();
    this.getNavs();
    this.getDropDownNavs();
  }
  navText!: string;
  navs!: Nav[];
  dropDownNavs!: DropDownNav[];

  getNavs() {
    this.navs = this.navbarService.getNavs();
  }

  getDropDownNavs() {
    this.dropDownNavs = this.navbarService.getDropDownNavs();
  }

  getNavText() {
    this.navText = this.navbarService.getNavText();
  }
}
