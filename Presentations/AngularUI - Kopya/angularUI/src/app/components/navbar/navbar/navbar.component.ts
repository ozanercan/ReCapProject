import { invalid } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit } from '@angular/core';
import { IDropDownNav } from 'src/app/models/navbars/IDropDownNav';
import { INav } from 'src/app/models/navbars/INav';
import { Nav } from 'src/app/models/navbars/nav';
import { NavbarService } from 'src/app/services/navbar/navbar.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private navbarService: NavbarService) {}

  ngOnInit(): void {
    this.navs = this.navbarService.getNavs();
    this.dropDownNavs = this.navbarService.getDropDownNavs();

    this.navbarBrandTitle = this.navbarService.NavbarBrandTitle;
  }

  navs: INav[] = [];
  dropDownNavs: IDropDownNav[] = [];

  navbarBrandTitle!:string;
}
