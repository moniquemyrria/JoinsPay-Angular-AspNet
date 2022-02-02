import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeriodDateComponent } from './period-date.component';

describe('PeriodDateComponent', () => {
  let component: PeriodDateComponent;
  let fixture: ComponentFixture<PeriodDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PeriodDateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PeriodDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
