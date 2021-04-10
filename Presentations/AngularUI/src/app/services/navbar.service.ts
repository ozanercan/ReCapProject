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
    {
      title: 'Ana Sayfa',
      route: 'home/cars',
      onlyAuthentication: false,
      onlyClaim: undefined,
    },
  ];

  private dropDownNavs: DropDownNav[] = [
    {
      title: 'Araç İşlemleri',
      onlyAuthentication: false,
      onlyClaim: undefined,
      childNavs: [
        {
          title: 'Tablolu Liste',
          route: 'car/list/table',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
        },
        {
          title: 'Renge Göre Liste',
          route: 'car/list/color/card',
          onlyAuthentication: false,
          onlyClaim: undefined,
        },
        {
          title: 'Markaya Göre Liste',
          route: 'car/list/brand/card',
          onlyAuthentication: false,
          onlyClaim: undefined,
        },
        {
          title: 'Gelişmiş Liste',
          route: 'car/list/filter/card',
          onlyAuthentication: false,
          onlyClaim: undefined,
        },
        {
          title: 'Yeni Araç Oluştur',
          route: 'car/add/form',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
        },
      ],
    },
    {
      title: 'Marka İşlemleri',
      onlyAuthentication: true,
      onlyClaim: ['admin'],
      childNavs: [
        {
          title: 'Listele',
          route: 'brand/list/table',
          onlyAuthentication: false,
          onlyClaim: ['admin'],
        },
        {
          title: 'Oluştur',
          route: 'brand/add/form',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
        },
      ],
    },
    {
      title: 'Renk İşlemleri',
      onlyAuthentication: true,
      onlyClaim: ['admin'],
      childNavs: [
        {
          title: 'Listele',
          route: 'color/list/table',
          onlyAuthentication: false,
          onlyClaim: ['admin'],
        },
        {
          title: 'Oluştur',
          route: 'color/add/form',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
        },
      ],
    },
    {
      title: 'Müşteri İşlemleri',
      onlyAuthentication: true,
      onlyClaim: ['admin'],
      childNavs: [
        {
          title: 'Listele',
          route: 'customer/list/table',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
        },
      ],
    },
    {
      title: 'Kiralama İşlemleri',
      onlyAuthentication: true,
      onlyClaim: ['admin'],
      childNavs: [
        {
          title: 'Listele',
          route: 'rental/list/table',
          onlyAuthentication: true,
          onlyClaim: ['admin'],
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
    let dropDownList: DropDownNav[] = this.dropDownNavs.splice(
      0,
      this.dropDownNavs.length
    );

    // Giriş Yapılmışsa
    if (this.authService.isAuthentication()) {
      dropDownList = this.GetAuthorizedOrNonAuthorizeDropDownNav(dropDownList);
    } else {
      dropDownList = this.getNonAuthorizeOrNonAuthenticationDropDownNav(
        dropDownList
      );
    }
    return dropDownList;
  }

  private GetAuthorizedOrNonAuthorizeDropDownNav(dropDownList: DropDownNav[]) {
    dropDownList = dropDownList.filter(
      (p) =>
        this.authService.isUserHaveClaims(p.onlyClaim!) ||
        p.onlyClaim == undefined
    );

    dropDownList.forEach((p) => {
      p.childNavs = p.childNavs.filter(
        (p) =>
          this.authService.isUserHaveClaims(p.onlyClaim!) ||
          p.onlyClaim == undefined
      );
    });
    return dropDownList;
  }

  private getNonAuthorizeOrNonAuthenticationDropDownNav(
    dropDownList: DropDownNav[]
  ) {
    dropDownList = dropDownList.filter(
      (p) => p.onlyClaim == undefined || p.onlyAuthentication === false
    );

    dropDownList.forEach((p) => {
      p.childNavs = p.childNavs.filter(
        (p) => p.onlyClaim == undefined || p.onlyAuthentication === false
      );
    });
    return dropDownList;
  }
}
