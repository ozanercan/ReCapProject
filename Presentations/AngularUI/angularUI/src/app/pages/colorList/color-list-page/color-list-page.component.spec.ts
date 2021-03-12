import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorListPageComponent } from './color-list-page.component';

describe('ColorListPageComponent', () => {
  let component: ColorListPageComponent;
  let fixture: ComponentFixture<ColorListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColorListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ColorListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
