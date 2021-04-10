import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrandListWithTableComponent } from './components/brand-list-with-table/brand-list-with-table.component';
import { BrandListWithTablePageComponent } from './pages/brand-list-with-table-page/brand-list-with-table-page.component';
import { ColorListWithTableComponent } from './components/color-list-with-table/color-list-with-table.component';
import { ColorListWithTablePageComponent } from './pages/color-list-with-table-page/color-list-with-table-page.component';
import { CustomerListWithTableComponent } from './components/customer-list-with-table/customer-list-with-table.component';
import { CustomerListWithTablePageComponent } from './pages/customer-list-with-table-page/customer-list-with-table-page.component';
import { CarListWithTableComponent } from './components/car-list-with-table/car-list-with-table.component';
import { CarListWithTablePageComponent } from './pages/car-list-with-table-page/car-list-with-table-page.component';
import { RentalListWithTableComponent } from './components/rental-list-with-table/rental-list-with-table.component';
import { RentalListWithTablePageComponent } from './pages/rental-list-with-table-page/rental-list-with-table-page.component';
import { BrandListVerticalComponent } from './components/brand-list-vertical/brand-list-vertical.component';
import { CarCardComponent } from './components/car-card/car-card.component';
import { CarListByParametersPageComponent } from './pages/car-list-by-parameters-page/car-list-by-parameters-page.component';
import { CarListWithCardComponent } from './components/car-list-with-card/car-list-with-card.component';
import { ColorListHorizontalComponent } from './components/color-list-horizontal/color-list-horizontal.component';
import { CarDetailComponent } from './components/car-detail/car-detail.component';
import { NavbarComponent } from './components/navbar/navbar.component';

import { ColorFilterPipe } from './pipes/color-filter.pipe';
import { BrandFilterPipe } from './pipes/brand-filter.pipe';
import { CarFilterPipe } from './pipes/car-filter.pipe';
import { ColorListVerticalComponent } from './components/color-list-vertical/color-list-vertical.component';
import { CarListByColorPageComponent } from './pages/car-list-by-color-page/car-list-by-color-page.component';
import { CarListByBrandPageComponent } from './pages/car-list-by-brand-page/car-list-by-brand-page.component';
import { RentalNewPageComponent } from './pages/rental-new-page/rental-new-page.component';
import { PaymentComponent } from './components/payment/payment.component';
import { BrandAddWithFormComponent } from './components/brand-add-with-form/brand-add-with-form.component';
import { ColorAddWithFormComponent } from './components/color-add-with-form/color-add-with-form.component';
import { CarAddWithFormComponent } from './components/car-add-with-form/car-add-with-form.component';
import { BrandUpdateWithFormComponent } from './components/brand-update-with-form/brand-update-with-form.component';
import { ColorUpdateWithFormComponent } from './components/color-update-with-form/color-update-with-form.component';
import { CarUpdateWithFormComponent } from './components/car-update-with-form/car-update-with-form.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthorizationInterceptor } from './interceptors/authorization.interceptor';
import { AuthenticationGuard } from './guards/authentication.guard';
import { LoginWithModalComponent } from './components/login-with-modal/login-with-modal.component';
import { CustomerUpdateSelfWithFormComponent } from './components/customer-update-self-with-form/customer-update-self-with-form.component';
import { LoginGuard } from './guards/login.guard';
import { RegisterGuard } from './guards/register.guard';
import { NavbarInUserComponent } from './components/navbar-in-user/navbar-in-user.component';
import { OnlyNumbersDirective } from './directives/only-numbers.directive';
import { CarBannerComponent } from './components/car-banner/car-banner.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { CarImageAddWithFormComponent } from './components/car-image-add-with-form/car-image-add-with-form.component';
import { CarImageUpdateWithFormComponent } from './components/car-image-update-with-form/car-image-update-with-form.component';
import { CarUpdatePageComponent } from './pages/car-update-page/car-update-page.component';

@NgModule({
  declarations: [
    AppComponent,
    BrandListWithTableComponent,
    BrandListWithTablePageComponent,
    ColorListWithTableComponent,
    ColorListWithTablePageComponent,
    CustomerListWithTableComponent,
    CustomerListWithTablePageComponent,
    CarListWithTableComponent,
    CarListWithTablePageComponent,
    RentalListWithTableComponent,
    RentalListWithTablePageComponent,
    BrandListVerticalComponent,
    CarCardComponent,
    CarListByParametersPageComponent,
    CarListWithCardComponent,
    ColorListHorizontalComponent,
    ColorListVerticalComponent,
    CarDetailComponent,
    NavbarComponent,
    ColorFilterPipe,
    BrandFilterPipe,
    CarFilterPipe,
    CarListByColorPageComponent,
    CarListByBrandPageComponent,
    RentalNewPageComponent,
    PaymentComponent,
    BrandAddWithFormComponent,
    ColorAddWithFormComponent,
    CarAddWithFormComponent,
    BrandUpdateWithFormComponent,
    ColorUpdateWithFormComponent,
    CarUpdateWithFormComponent,
    LoginComponent,
    RegisterComponent,
    LoginWithModalComponent,
    CustomerUpdateSelfWithFormComponent,
    NavbarInUserComponent,
    OnlyNumbersDirective,
    CarBannerComponent,
    HomePageComponent,
    CarImageAddWithFormComponent,
    CarUpdatePageComponent,
    CarImageUpdateWithFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      newestOnTop: false,
      enableHtml: true
    }),
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationInterceptor,
      multi: true,
    }, AuthenticationGuard, LoginGuard, RegisterGuard
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
