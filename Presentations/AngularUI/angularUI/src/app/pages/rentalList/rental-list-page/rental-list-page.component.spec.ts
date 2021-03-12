import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentalListPageComponent } from './rental-list-page.component';

describe('RentalListPageComponent', () => {
  let component: RentalListPageComponent;
  let fixture: ComponentFixture<RentalListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentalListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
