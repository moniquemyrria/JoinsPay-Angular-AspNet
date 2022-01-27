import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueCategoryListComponent } from './revenue-category-list.component';

describe('RevenueCategoryComponent', () => {
  let component: RevenueCategoryListComponent;
  let fixture: ComponentFixture<RevenueCategoryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevenueCategoryListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevenueCategoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
