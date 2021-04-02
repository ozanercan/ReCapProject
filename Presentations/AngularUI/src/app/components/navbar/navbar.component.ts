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
  ngOnInit(): void {}

  navbarBrandText = 'Rental';

  navs: Nav[] = [{ title: 'Ana Sayfa', route: '' }];

  dropDownNavs: DropDownNav[] = [
    {
      title: 'Araç İşlemleri',
      childNavs: [
        { title: 'Tablolu Liste', route: 'car/list/table' },
        { title: 'Renge Göre Liste', route: 'car/list/color/card' },
        {
          title: 'Markaya Göre Liste',
          route: 'car/list/brand/card',
        },
        {
          title: 'Gelişmiş Liste',
          route: 'car/list/filter/card',
        },
        {
          title: 'Yeni Araç Oluştur',
          route: 'car/add/form',
        },
      ],
    },
    {
      title: 'Marka İşlemleri',
      childNavs: [
        { title: 'Listele', route: 'brand/list/table' },
        { title: 'Oluştur', route: 'brand/add/form' },
      ],
    },
    {
      title: 'Renk İşlemleri',
      childNavs: [
        { title: 'Listele', route: 'color/list/table' },
        { title: 'Oluştur', route: 'color/add/form' },
      ],
    },
    {
      title: 'Müşteri İşlemleri',
      childNavs: [{ title: 'Listele', route: 'customer/list/table' }],
    },
    {
      title: 'Kiralama İşlemleri',
      childNavs: [{ title: 'Listele', route: 'rental/list/table' }],
    },
  ];
}
