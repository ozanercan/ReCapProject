import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarInUserComponent } from './navbar-in-user.component';

describe('NavbarInUserComponent', () => {
  let component: NavbarInUserComponent;
  let fixture: ComponentFixture<NavbarInUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarInUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarInUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
