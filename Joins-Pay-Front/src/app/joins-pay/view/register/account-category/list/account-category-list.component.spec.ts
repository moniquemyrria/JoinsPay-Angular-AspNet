import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountCategoryListComponent } from './account-category-list.component';

describe('AccountCategoryComponent', () => {
  let component: AccountCategoryListComponent;
  let fixture: ComponentFixture<AccountCategoryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountCategoryListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountCategoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
