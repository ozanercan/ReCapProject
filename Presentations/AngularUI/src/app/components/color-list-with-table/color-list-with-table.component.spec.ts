import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorListWithTableComponent } from './color-list-with-table.component';

describe('ColorListWithTableComponent', () => {
  let component: ColorListWithTableComponent;
  let fixture: ComponentFixture<ColorListWithTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColorListWithTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ColorListWithTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
