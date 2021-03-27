import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private authService:AuthService,
     private rememberMeService:RememberMeService,
     private userService:UserService,
     private router:Router ) {
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

  getLoggedInUserNames() {
    this.userService.getFirstLastNameByEmail((this.rememberMeService.getEmail()!)).subscribe(
      response=>{
        console.log(response)
        this.loggedInUser = response.data;
      }
    );
  }

  logout(){
    this.authService.logout();
  }

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
