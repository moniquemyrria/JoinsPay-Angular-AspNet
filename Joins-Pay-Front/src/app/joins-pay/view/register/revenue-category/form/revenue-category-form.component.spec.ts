import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueCategoryFormComponent } from './revenue-category-form.component';

describe('RevenueCategoryFormComponent', () => {
  let component: RevenueCategoryFormComponent;
  let fixture: ComponentFixture<RevenueCategoryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevenueCategoryFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevenueCategoryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
