import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerListWithTableComponent } from './customer-list-with-table.component';

describe('CustomerListWithTableComponent', () => {
  let component: CustomerListWithTableComponent;
  let fixture: ComponentFixture<CustomerListWithTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerListWithTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerListWithTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
