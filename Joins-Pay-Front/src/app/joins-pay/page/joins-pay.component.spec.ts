import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinsPayComponent } from './joins-pay.component';


describe('JoinsPayComponent', () => {
  let component: JoinsPayComponent;
  let fixture: ComponentFixture<JoinsPayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinsPayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JoinsPayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
