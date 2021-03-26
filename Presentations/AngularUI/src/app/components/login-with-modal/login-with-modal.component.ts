import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { LoginDto } from 'src/app/models/Dtos/loginDto';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-login-with-modal',
  templateUrl: './login-with-modal.component.html',
  styleUrls: ['./login-with-modal.component.css']
})
export class LoginWithModalComponent implements OnInit {

  constructor( private authService: AuthService,
    private toastrService: ToastrService,
    private tokenService:TokenService,
    private formBuilder: FormBuilder,
    private rememberMeService:RememberMeService) { }

  ngOnInit(): void {
    this.createLoginForm();
  }

  loginForm!:FormGroup;
  createLoginForm(){
    this.loginForm = this.formBuilder.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  login(){
    console.log(this.loginForm);
    if(this.loginForm.valid){
      let loginDto : LoginDto = this.loginForm.value;
      this.authService.login(loginDto).subscribe(response=>{

        this.tokenService.setToken(response.data);
        this.rememberMeService.setEmail(loginDto.email);
        
        this.toastrService.success('Giriş Yapıldı');
      }, errorResponse=>{
        this.toastrService.error(ErrorHelper.getMessage(errorResponse), 'Hata');
      });
    }
    else{
      this.toastrService.warning('Lütfen gerekli alanları istenilen biçimde doldurunuz.','Model Doğrulama Uyarısı');
    }
  }
}
