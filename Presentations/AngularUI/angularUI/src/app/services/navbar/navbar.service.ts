import { Injectable } from '@angular/core';
import { DropDownNav } from 'src/app/models/navbars/dropDownNav';
import { IDropDownNav } from 'src/app/models/navbars/IDropDownNav';
import { INav } from 'src/app/models/navbars/INav';
import { Nav } from 'src/app/models/navbars/nav';

@Injectable({
  providedIn: 'root',
})
export class NavbarService {
  constructor() {
    this.addAllNav();
    this.addAllDropDownNav();
  }

  NavbarBrandTitle: string = 'Rental';

  private navs: INav[] = [];
  private dropDownNavs: IDropDownNav[] = [];

  private addAllNav() {
    this.addNav(new Nav('Anasayfa', '#'));
  }

  private addAllDropDownNav() {
    let customerChildNavs: Nav[] = [
      new Nav('Müşterileri Listele', '/customer-list'),
    ];
    let brandChildNavs: Nav[] = [new Nav('Markaları Listele', '/brand-list')];
    let colorChildNavs: Nav[] = [new Nav('Renkleri Listele', '/color-list')];
    let carChildNavs: Nav[] = [new Nav('Arabaları Listele', '/car-list')];

    this.addDropDownNavs(
      new DropDownNav('Müşteri İşlemleri', '#', customerChildNavs),
      new DropDownNav('Marka İşlemleri', '#', brandChildNavs),
      new DropDownNav('Renk İşlemleri', '#', colorChildNavs),
      new DropDownNav('Araba İşlemleri', '#', carChildNavs)
    );
  }

  addNav(nav: Nav) {
    this.navs.push(nav);
  }
  addNavs(...nav: Nav[]) {
    nav.forEach((p) => {
      this.navs.push(p);
    });
  }

  getNavs(): INav[] {
    return this.navs;
  }

  addDropDownNav(dropDownNav: IDropDownNav) {
    this.dropDownNavs.push(dropDownNav);
  }
  addDropDownNavs(...dropDownNav: IDropDownNav[]) {
    dropDownNav.forEach((dropDownNav) => {
      this.dropDownNavs.push(dropDownNav);
    });
  }

  getDropDownNavs(): IDropDownNav[] {
    return this.dropDownNavs;
  }
}
