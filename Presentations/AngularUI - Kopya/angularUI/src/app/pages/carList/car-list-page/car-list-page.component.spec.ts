import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarListPageComponent } from './car-list-page.component';

describe('CarListPageComponent', () => {
  let component: CarListPageComponent;
  let fixture: ComponentFixture<CarListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
