import { Component, OnInit } from '@angular/core';
import { CustomerFirstLastNameDto } from 'src/app/models/Dtos/customerFirstLastNameDto';
import { DropDownNav } from 'src/app/models/navbar/dropDownNav';
import { Nav } from 'src/app/models/navbar/nav';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private authService:AuthService, private rememberMeService:RememberMeService, private userService:UserService) {
  }
  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthentication();
    if(this.isLoggedIn){
      this.getLoggedInUserNames();
    }
  }
  isLoggedIn:Boolean = false;
  loggedInUser!:CustomerFirstLastNameDto;
  navbarBrandText = 'Rental';
  navs: Nav[] = [{ title: 'Ana Sayfa', route: '' }];

  getLoggedInUserNames() {
    this.userService.getFirstLastNameByEmail((this.rememberMeService.getEmail()!)).subscribe(
      response=>{
        console.log(response)
        this.loggedInUser = response.data;
      }
    );
  }
  dropDownNavs: DropDownNav[] = [
    {
      title: 'Araç İşlemleri',
      childNavs: [
        { title: 'Tablolu Liste', route: 'carListWithTable' },
        { title: 'Renge Göre Liste', route: 'carListByColor/carListWithCard' },
        {
          title: 'Markaya Göre Liste',
          route: 'carListByBrand/carListWithCard',
        },
        {
          title: 'Gelişmiş Liste',
          route: 'carListByParameters/carListWithCard',
        },
        {
          title: 'Yeni Araç Oluştur',
          route: 'carAddWithForm',
        },
      ],
    },
    {
      title: 'Marka İşlemleri',
      childNavs: [
        { title: 'Listele', route: 'brandListWithTable' },
        { title: 'Oluştur', route: 'brandAddWithForm' },
      ],
    },
    {
      title: 'Renk İşlemleri',
      childNavs: [
        { title: 'Listele', route: 'colorListWithTable' },
        { title: 'Oluştur', route: 'colorAddWithForm' },
      ],
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
  
}
