import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseFixedFormComponent } from './expense-fixed-form.component';

describe('ExpenseFixedFormComponent', () => {
  let component: ExpenseFixedFormComponent;
  let fixture: ComponentFixture<ExpenseFixedFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseFixedFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseFixedFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
