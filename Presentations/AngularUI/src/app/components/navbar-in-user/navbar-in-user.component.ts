import { Component, Input, OnInit } from '@angular/core';
import { CustomerFirstLastNameDto } from 'src/app/models/Dtos/customerFirstLastNameDto';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar-in-user',
  templateUrl: './navbar-in-user.component.html',
  styleUrls: ['./navbar-in-user.component.css'],
})
export class NavbarInUserComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private rememberMeService:RememberMeService
  ) {}

  isLoggedIn: boolean = false;

  loggedInUser!: CustomerFirstLastNameDto;

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthentication();
    if (this.isLoggedIn) {
      this.getLoggedInUserNames();
    }
  }

  getLoggedInUserNames() {
    this.userService
      .getFirstLastNameByEmail(this.rememberMeService.getEmail()!)
      .subscribe((response) => {
        console.log(response);
        this.loggedInUser = response.data;
      });
  }

  logout(){
    this.authService.logout();
  }
}
