import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarDetailComponent } from './components/car-detail/car-detail.component';
import { CarListWithCardComponent } from './components/car-list-with-card/car-list-with-card.component';
import { BrandListWithTablePageComponent } from './pages/brand-list-with-table-page/brand-list-with-table-page.component';
import { CarListByParametersPageComponent } from './pages/car-list-by-parameters-page/car-list-by-parameters-page.component';
import { CarListWithTablePageComponent } from './pages/car-list-with-table-page/car-list-with-table-page.component';
import { ColorListWithTablePageComponent } from './pages/color-list-with-table-page/color-list-with-table-page.component';
import { CustomerListWithTablePageComponent } from './pages/customer-list-with-table-page/customer-list-with-table-page.component';
import { RentalListWithTablePageComponent } from './pages/rental-list-with-table-page/rental-list-with-table-page.component';

const routes: Routes = [
  { path: 'brandListWithTable', component: BrandListWithTablePageComponent },
  { path: 'colorListWithTable', component: ColorListWithTablePageComponent },
  {
    path: 'customerListWithTable',
    component: CustomerListWithTablePageComponent,
  },
  { path: 'carListWithTable', component: CarListWithTablePageComponent },
  { path: 'rentalListWithTable', component: RentalListWithTablePageComponent },
  { path: 'carDetail/car/:carId', component: CarDetailComponent },
  { path: '', pathMatch:'full', redirectTo: 'carListByParameters/carListWithCard' },
  {
    path: 'carListByParameters',
    component: CarListByParametersPageComponent,
    children: [
      {
        path: 'carListWithCard',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/brand/:brandId',
        component: CarListWithCardComponent,
      },
      {
        path: 'carListWithCard/color/:colorId',
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
