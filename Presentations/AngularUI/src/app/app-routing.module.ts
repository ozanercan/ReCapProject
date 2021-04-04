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
import { AuthenticationGuard } from './guards/authentication.guard';
import { LoginGuard } from './guards/login.guard';
import { RegisterGuard } from './guards/register.guard';
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
  {
    path: 'customer/update/form',
    component: CustomerUpdateSelfWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'customer/register',
    component: RegisterComponent,
    canActivate: [RegisterGuard],
  },
  {
    path: 'customer/login/modal',
    component: LoginWithModalComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'customer/login/form',
    component: LoginComponent,
    canActivate: [LoginGuard],
  },
  { path: 'imageUpload', component: ImageUploadComponent },
  {
    path: 'brand/add/form',
    component: BrandAddWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'brand/update/form/:brandId',
    component: BrandUpdateWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  { path: 'brand/list/table', component: BrandListWithTablePageComponent },
  {
    path: 'car/add/form',
    component: CarAddWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'car/update/form/:carId',
    component: CarUpdateWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  { path: 'color/list/table', component: ColorListWithTablePageComponent },
  {
    path: 'color/update/form/:colorId',
    component: ColorUpdateWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'customer/list/table',
    component: CustomerListWithTablePageComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'color/add/form',
    component: ColorAddWithFormComponent,
    canActivate: [AuthenticationGuard],
  },
  { path: 'car/list/table', component: CarListWithTablePageComponent },
  { path: 'rental/list/table', component: RentalListWithTablePageComponent },
  { path: 'car/detail/:carId', component: CarDetailComponent },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'car/list/filter/card',
  },
  {
    path: 'rental/add/:carId',
    component: RentalNewPageComponent,
    
  },
  {
    path: 'payment/add/:rentalId',
    component: PaymentComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'car/list/color',
    component: CarListByColorPageComponent,
    children: [
      {
        path: 'card',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/:colorId',
        component: CarListWithCardComponent,
      },
    ],
  },
  {
    path: 'car/list/brand',
    component: CarListByBrandPageComponent,
    children: [
      {
        path: 'card',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/:brandId',
        component: CarListWithCardComponent,
      },
    ],
  },
  {
    path: 'car/list/filter',
    component: CarListByParametersPageComponent,
    children: [
      {
        path: 'card',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/brandName/:brandName',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/brandId/:brandId',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/colorId/:colorId',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/colorName/:colorName',
        component: CarListWithCardComponent,
      },
      {
        path: 'card/:colorName/:brandName',
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
