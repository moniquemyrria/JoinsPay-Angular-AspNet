import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountCategoryFormComponent } from './account-category-form.component';

describe('AccountCategoryFormComponent', () => {
  let component: AccountCategoryFormComponent;
  let fixture: ComponentFixture<AccountCategoryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountCategoryFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountCategoryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
