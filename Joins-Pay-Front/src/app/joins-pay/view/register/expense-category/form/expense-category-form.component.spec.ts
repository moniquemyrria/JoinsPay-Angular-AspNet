import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseCategoryFormComponent } from './expense-category-form.component';

describe('ExpenseCategoryFormComponent', () => {
  let component: ExpenseCategoryFormComponent;
  let fixture: ComponentFixture<ExpenseCategoryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseCategoryFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseCategoryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
