import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandListPageComponent } from './brand-list-page.component';

describe('BrandListPageComponent', () => {
  let component: BrandListPageComponent;
  let fixture: ComponentFixture<BrandListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrandListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
