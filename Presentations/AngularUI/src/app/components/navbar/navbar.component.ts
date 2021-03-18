import { Component, OnInit } from '@angular/core';
import { DropDownNav } from 'src/app/models/navbar/dropDownNav';
import { Nav } from 'src/app/models/navbar/nav';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor() {}

  navbarBrandText: string = 'Rental';
  navs: Nav[] = [{ title: 'Ana Sayfa', route: '' }];

  dropDownNavs: DropDownNav[] = [
    {
      title: 'Araç İşlemleri',
      childNavs: [
        { title: 'Tablolu Liste', route: 'carListWithTable' },
        { title: 'Renge Göre Liste', route: 'carListByColor' },
        { title: 'Markaya Göre Liste', route: 'carListByBrand' },
        { title: 'Gelişmiş Liste', route: 'carListByParameters/carListWithCard' }
      ],
    },
    {
      title: 'Marka İşlemleri',
      childNavs: [{ title: 'Listele', route: 'brandListWithTable' }],
    },
    {
      title: 'Renk İşlemleri',
      childNavs: [{ title: 'Listele', route: 'colorListWithTable' }],
    },
    {
      title: 'Müşteri İşlemleri',
      childNavs: [{ title: 'Listele', route: 'customerListWithTable' }],
    },
    {
      title: 'Kiralama İşlemleri',
      childNavs: [{ title: 'Listele', route: 'rentalListWithTable' }],
    },
  ];
  ngOnInit(): void {}
}
