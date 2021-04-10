import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer } from 'rxjs';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { LoginDto } from 'src/app/models/Dtos/loginDto';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { TitleService } from 'src/app/services/title.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private tokenService:TokenService,
    private formBuilder: FormBuilder,
    private rememberMeService:RememberMeService,
    private router:Router,
    private titleService: TitleService
  ) {}

  loginForm!:FormGroup;

  ngOnInit(): void {
    this.titleService.setTitle('Giriş Yap');
    this.createLoginForm();
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  login(){
    if(this.loginForm.valid){
      let loginDto : LoginDto = this.loginForm.value;
      this.authService.login(loginDto).subscribe(response=>{
        this.tokenService.setToken(response.data);
        this.rememberMeService.setEmail(loginDto.email);
        this.rememberMeService.setUser(response.data.user);
        this.toastrService.success('Giriş yapıldı.');

        this.toastrService.info('Ana Sayfaya yönlendiriliyorsunuz.');

        timer(3000).subscribe(p=>{
          this.router.navigate(['']);
        });

      }, errorResponse=>{
        this.toastrService.error(ErrorHelper.getMessage(errorResponse), 'Hata');
      });
    }
    else{
      this.toastrService.warning('Lütfen gerekli alanları istenilen biçimde doldurunuz.','Model Doğrulama Uyarısı');
    }
  }
}
