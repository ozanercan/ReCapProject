import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandListPageComponent } from './pages/brandList/brand-list-page/brand-list-page.component';
import { CarListPageComponent } from './pages/carList/car-list-page/car-list-page.component';
import { ColorListPageComponent } from './pages/colorList/color-list-page/color-list-page.component';
import { CustomerListPageComponent } from './pages/customerList/customer-list-page/customer-list-page.component';
import { RentalListPageComponent } from './pages/rentalList/rental-list-page/rental-list-page.component';

const routes: Routes = [
  { path: 'brand-list', component: BrandListPageComponent },
  { path: 'color-list', component: ColorListPageComponent },
  { path: 'customer-list', component: CustomerListPageComponent },
  { path: 'car-list', component: CarListPageComponent },
  { path: 'rental-list', component: RentalListPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
