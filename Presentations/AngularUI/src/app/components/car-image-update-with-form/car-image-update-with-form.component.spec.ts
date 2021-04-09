import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarImageUpdateWithFormComponent } from './car-image-update-with-form.component';

describe('CarImageUpdateWithFormComponent', () => {
  let component: CarImageUpdateWithFormComponent;
  let fixture: ComponentFixture<CarImageUpdateWithFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarImageUpdateWithFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarImageUpdateWithFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
