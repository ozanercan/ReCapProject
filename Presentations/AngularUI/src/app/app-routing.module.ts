import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandAddWithFormComponent } from './components/brand-add-with-form/brand-add-with-form.component';
import { BrandUpdateWithFormComponent } from './components/brand-update-with-form/brand-update-with-form.component';
import { CarAddWithFormComponent } from './components/car-add-with-form/car-add-with-form.component';
import { CarCardComponent } from './components/car-card/car-card.component';
import { CarDetailComponent } from './components/car-detail/car-detail.component';
import { CarImageAddWithFormComponent } from './components/car-image-add-with-form/car-image-add-with-form.component';
import { CarImageUpdateWithFormComponent } from './components/car-image-update-with-form/car-image-update-with-form.component';
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
import { AuthorizationGuard } from './guards/authorization.guard';
import { LoginGuard } from './guards/login.guard';
import { RegisterGuard } from './guards/register.guard';
import { BrandListWithTablePageComponent } from './pages/brand-list-with-table-page/brand-list-with-table-page.component';
import { CarListByBrandPageComponent } from './pages/car-list-by-brand-page/car-list-by-brand-page.component';
import { CarListByColorPageComponent } from './pages/car-list-by-color-page/car-list-by-color-page.component';
import { CarListByParametersPageComponent } from './pages/car-list-by-parameters-page/car-list-by-parameters-page.component';
import { CarListWithTablePageComponent } from './pages/car-list-with-table-page/car-list-with-table-page.component';
import { CarUpdatePageComponent } from './pages/car-update-page/car-update-page.component';
import { ColorListWithTablePageComponent } from './pages/color-list-with-table-page/color-list-with-table-page.component';
import { CustomerListWithTablePageComponent } from './pages/customer-list-with-table-page/customer-list-with-table-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { RentalListWithTablePageComponent } from './pages/rental-list-with-table-page/rental-list-with-table-page.component';
import { RentalNewPageComponent } from './pages/rental-new-page/rental-new-page.component';

const routes: Routes = [
  // {
  //   path: '',
  //   pathMatch: 'full',
  //   redirectTo: 'home',
  // },
  { path: 'home', component: HomePageComponent },
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
  {
    path: 'customer/list/table',
    component: CustomerListWithTablePageComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'admin/brand/add/form',
    component: BrandAddWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },
  {
    path: 'brand/update/form/:brandId',
    component: BrandUpdateWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },
  { path: 'brand/list/table', component: BrandListWithTablePageComponent },
  {
    path: 'car/add/form',
    component: CarAddWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },
  {
    path: 'car/update/form/:carId',
    component: CarUpdateWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },
  {
    path: 'carUpdate/:carId',
    component: CarUpdatePageComponent,
  },
  {
    path: 'car/list/color',
    component: CarListByColorPageComponent,
    children: [
      {
        path: 'card',
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
    ],
  },
  { path: 'car/detail/:carId', component: CarDetailComponent },
  { path: 'car/list/table', component: CarListWithTablePageComponent },
  {
    path: 'carimage/update/:carId',
    component: CarImageUpdateWithFormComponent,
  },
  { path: 'carimage/add/:carId', component: CarImageAddWithFormComponent },

  { path: 'color/list/table', component: ColorListWithTablePageComponent },
  {
    path: 'color/update/form/:colorId',
    component: ColorUpdateWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },

  {
    path: 'color/add/form',
    component: ColorAddWithFormComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
    data: { roles: ['admin'] },
  },

  { path: 'rental/list/table', component: RentalListWithTablePageComponent },

  {
    path: 'rental/add/:carId',
    component: RentalNewPageComponent,
  },
  {
    path: 'payment/add/:rentalId',
    component: PaymentComponent,
    canActivate: [AuthenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
