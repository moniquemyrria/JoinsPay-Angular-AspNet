import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseCategoryListComponent } from './expense-category-list.component';

describe('ExpenseCategoryComponent', () => {
  let component: ExpenseCategoryListComponent;
  let fixture: ComponentFixture<ExpenseCategoryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseCategoryListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseCategoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
