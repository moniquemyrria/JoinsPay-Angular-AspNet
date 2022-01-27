import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalAlertsMessageComponent } from './modal-alerts-message.component';

describe('ModalAlertsMessageComponent', () => {
  let component: ModalAlertsMessageComponent;
  let fixture: ComponentFixture<ModalAlertsMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalAlertsMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalAlertsMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
