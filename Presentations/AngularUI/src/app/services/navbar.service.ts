import { Injectable } from '@angular/core';
import { DropDownNav } from '../models/navbar/dropDownNav';
import { Nav } from '../models/navbar/nav';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class NavbarService {
  constructor(private authService: AuthService) {}

  private navbarBrandText = 'Rental';

  private navs: Nav[] = [
    { title: 'Ana Sayfa', route: '', onlyAuthentication: false },
  ];

  private dropDownNavs: DropDownNav[] = [
    {
      title: 'Araç İşlemleri',
      onlyAuthentication: false,
      childNavs: [
        {
          title: 'Tablolu Liste',
          route: 'car/list/table',
          onlyAuthentication: false,
        },
        {
          title: 'Renge Göre Liste',
          route: 'car/list/color/card',
          onlyAuthentication: false,
        },
        {
          title: 'Markaya Göre Liste',
          route: 'car/list/brand/card',
          onlyAuthentication: false,
        },
        {
          title: 'Gelişmiş Liste',
          route: 'car/list/filter/card',
          onlyAuthentication: false,
        },
        {
          title: 'Yeni Araç Oluştur',
          route: 'car/add/form',
          onlyAuthentication: true,
        },
      ],
    },
    {
      title: 'Marka İşlemleri',
      onlyAuthentication: true,
      childNavs: [
        {
          title: 'Listele',
          route: 'brand/list/table',
          onlyAuthentication: false,
        },
        {
          title: 'Oluştur',
          route: 'brand/add/form',
          onlyAuthentication: true,
        },
      ],
    },
    {
      title: 'Renk İşlemleri',
      onlyAuthentication: true,
      childNavs: [
        {
          title: 'Listele',
          route: 'color/list/table',
          onlyAuthentication: false,
        },
        {
          title: 'Oluştur',
          route: 'color/add/form',
          onlyAuthentication: true,
        },
      ],
    },
    {
      title: 'Müşteri İşlemleri',
      onlyAuthentication: true,
      childNavs: [
        {
          title: 'Listele',
          route: 'customer/list/table',
          onlyAuthentication: true,
        },
      ],
    },
    {
      title: 'Kiralama İşlemleri',
      onlyAuthentication: true,
      childNavs: [
        {
          title: 'Listele',
          route: 'rental/list/table',
          onlyAuthentication: true,
        },
      ],
    },
  ];

  getNavText(): string {
    return this.navbarBrandText;
  }

  getNavs(): Nav[] {
    return this.navs;
  }

  getDropDownNavs(): DropDownNav[] {
    if (this.authService.isAuthentication()) {
      return this.dropDownNavs;
    }

    let dropDownList: DropDownNav[] = this.dropDownNavs
      .splice(0, this.dropDownNavs.length)
      .filter((p) => p.onlyAuthentication === false);

    dropDownList.forEach((element) => {
      element.childNavs = element.childNavs.filter(
        (p) => p.onlyAuthentication === false
      );
    });
    
    return dropDownList;
  }
}
