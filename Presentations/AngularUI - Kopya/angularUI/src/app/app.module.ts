import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrandListPageComponent } from './pages/brandList/brand-list-page/brand-list-page.component';
import { NavbarComponent } from './components/navbar/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { ColorListPageComponent } from './pages/colorList/color-list-page/color-list-page.component';
import { CustomerListPageComponent } from './pages/customerList/customer-list-page/customer-list-page.component';
import { CarListPageComponent } from './pages/carList/car-list-page/car-list-page.component';
import { RentalListPageComponent } from './pages/rentalList/rental-list-page/rental-list-page.component';
import { BrandListComponent } from './components/brand/brand-list/brand-list.component';
import { CarListComponent } from './components/car/car-list/car-list.component';
import { ColorListComponent } from './components/color/color-list/color-list.component';
import { CustomerListComponent } from './components/customer/customer-list/customer-list.component';
import { RentalListComponent } from './components/rental/rental-list/rental-list.component';

@NgModule({
  declarations: [
    AppComponent,
    BrandListPageComponent,
    NavbarComponent,
    ColorListPageComponent,
    CustomerListPageComponent,
    CarListPageComponent,
    RentalListPageComponent,
    BrandListComponent,
    CarListComponent,
    ColorListComponent,
    CustomerListComponent,
    RentalListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
