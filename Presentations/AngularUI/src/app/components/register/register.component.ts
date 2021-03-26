import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ErrorHelper } from 'src/app/helpers/errorHelper';
import { RegisterDto } from 'src/app/models/Dtos/registerDto';
import { AuthService } from 'src/app/services/auth.service';
import { RememberMeService } from 'src/app/services/remember-me.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private tokenService:TokenService,
    private rememberMeService:RememberMeService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }
  registerForm!: FormGroup;

  createRegisterForm(){
    this.registerForm = this.formBuilder.group({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });
  }

  register() {
    if(this.registerForm.valid){
      let registerDto:RegisterDto = this.registerForm.value;

      this.authService.register(registerDto).subscribe(response=>{

        this.tokenService.setToken(response.data);
        this.rememberMeService.setEmail(registerDto.email);

        this.toastrService.success('Kayıt İşlemi Tamamlandı.');
      }, errorResponse=>{
        this.toastrService.error(ErrorHelper.getMessage(errorResponse));
      });
    }
    else{
      this.toastrService.warning('Lütfen tüm alanları istenildiği şekilde doldurunuz.');
    }
  }
}
