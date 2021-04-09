import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarBannerComponent } from './car-banner.component';

describe('CarBannerComponent', () => {
  let component: CarBannerComponent;
  let fixture: ComponentFixture<CarBannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarBannerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
