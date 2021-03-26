import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandAddWithFormComponent } from './components/brand-add-with-form/brand-add-with-form.component';
import { BrandUpdateWithFormComponent } from './components/brand-update-with-form/brand-update-with-form.component';
import { CarAddWithFormComponent } from './components/car-add-with-form/car-add-with-form.component';
import { CarCardComponent } from './components/car-card/car-card.component';
import { CarDetailComponent } from './components/car-detail/car-detail.component';
import { CarListWithCardComponent } from './components/car-list-with-card/car-list-with-card.component';
import { CarUpdateWithFormComponent } from './components/car-update-with-form/car-update-with-form.component';
import { ColorAddWithFormComponent } from './components/color-add-with-form/color-add-with-form.component';
import { ColorUpdateWithFormComponent } from './components/color-update-with-form/color-update-with-form.component';
import { CustomerListWithTableComponent } from './components/customer-list-with-table/customer-list-with-table.component';
import { CustomerUpdateSelfWithFormComponent } from './components/customer-update-self-with-form/customer-update-self-with-form.component';
import { LoginWithModalComponent } from './components/login-with-modal/login-with-modal.component';
import { LoginComponent } from './components/login/login.component';
import { PaymentComponent } from './components/payment/payment.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginGuard } from './guards/login.guard';
import { BrandListWithTablePageComponent } from './pages/brand-list-with-table-page/brand-list-with-table-page.component';
import { CarListByBrandPageComponent } from './pages/car-list-by-brand-page/car-list-by-brand-page.component';
import { CarListByColorPageComponent } from './pages/car-list-by-color-page/car-list-by-color-page.component';
import { CarListByParametersPageComponent } from './pages/car-list-by-parameters-page/car-list-by-parameters-page.component';
import { CarListWithTablePageComponent } from './pages/car-list-with-table-page/car-list-with-table-page.component';
import { ColorListWithTablePageComponent } from './pages/color-list-with-table-page/color-list-with-table-page.component';
import { CustomerListWithTablePageComponent } from './pages/customer-list-with-table-page/customer-list-with-table-page.component';
import { ImageUploadComponent } from './pages/image-upload/image-upload.component';
import { RentalListWithTablePageComponent } from './pages/rental-list-with-table-page/rental-list-with-table-page.component';
import { RentalNewPageComponent } from './pages/rental-new-page/rental-new-page.component';

const routes: Routes = [
  { path: 'customerUpdateSelfWithForm', component:CustomerUpdateSelfWithFormComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'loginwithmodal', component: LoginWithModalComponent},
  { path: 'login', component: LoginComponent },
  { path: 'imageUpload', component: ImageUploadComponent },
  { path: 'brandAddWithForm', component: BrandAddWithFormComponent, canActivate: [LoginGuard] },
  { path: 'brandUpdateWithForm/:brandId', component: BrandUpdateWithFormComponent },
  { path: 'brandListWithTable', component: BrandListWithTablePageComponent },
  { path: 'carAddWithForm', component: CarAddWithFormComponent },
  { path: 'carUpdateWithForm/:carId', component: CarUpdateWithFormComponent },
  { path: 'colorListWithTable', component: ColorListWithTablePageComponent },
  { path: 'colorUpdateWithForm/:colorId', component: ColorUpdateWithFormComponent },
  { path: 'customerListWithTable', component: CustomerListWithTablePageComponent },
  { path: 'colorAddWithForm', component: ColorAddWithFormComponent },
  { path: 'carListWithTable', component: CarListWithTablePageComponent },
  { path: 'rentalListWithTable', component: RentalListWithTablePageComponent },
  { path: 'carDetail/car/:carId', component: CarDetailComponent },
  { path: 'carDetail/car/:carId', component: CarDetailComponent },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'carListByParameters/carListWithCard',
  },
  {
    path: 'rental/new/:carId',
    component: RentalNewPageComponent,
  },
  {
    path: 'payment/:rentalId',
    component: PaymentComponent,
  },
  {
    path: 'carListByColor',
    component: CarListByColorPageComponent,
    children: [
      {
        path: 'carListWithCard',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/color/:colorId',
        component: CarListWithCardComponent,
      },
    ],
  },
  {
    path: 'carListByBrand',
    component: CarListByBrandPageComponent,
    children: [
      {
        path: 'carListWithCard',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/brand/:brandId',
        component: CarListWithCardComponent,
      },
    ],
  },
  {
    path: 'carListByParameters',
    component: CarListByParametersPageComponent,
    children: [
      {
        path: 'carListWithCard',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/brandName/:brandName',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/brandId/:brandId',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/colorId/:colorId',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/colorName/:colorName',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/filters/:colorName/:brandName',
        component: CarListWithCardComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
