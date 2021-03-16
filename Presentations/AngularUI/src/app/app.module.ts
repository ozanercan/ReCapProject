import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
    CarDetailComponent,
    NavbarComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
