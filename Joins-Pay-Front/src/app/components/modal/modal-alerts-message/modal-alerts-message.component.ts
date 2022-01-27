import { Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import { Router } from '@angular/router';
import { AlertMessageModel } from './modal-alerts-message-model';


@Component({
  selector: 'app-modal-alerts-message',
  templateUrl: './modal-alerts-message.component.html',
  styleUrls: ['./modal-alerts-message.component.scss']
})
export class ModalAlertsMessageComponent implements OnInit, OnDestroy {

  @Input() alertMesssage: AlertMessageModel = {} as AlertMessageModel
  @Input() display: boolean = false

  @Output() eventEmmiterConfirm = new EventEmitter()
  @Output() eventEmmiterConfirmYes = new EventEmitter()
  @Output() eventEmmiterDanger = new EventEmitter()

  constructor(
    private router: Router,
  ){}

  closedAlertMessage(){
    this.eventEmmiterConfirm.emit();
  }

  actionConfirm(){
    this.eventEmmiterConfirm.emit(this.alertMesssage.confirmButtonEventEmmiter);

    if (this.alertMesssage.sendRouter){
      this.router.navigateByUrl(this.alertMesssage.confirmButtonEventEmmiter.routerLink);
    }
  }

  actionConfirmYes(){
    this.eventEmmiterConfirmYes.emit(this.alertMesssage.confirmButtonEventEmmiter);
  }

  actionDanger(){
    this.eventEmmiterDanger.emit(this.alertMesssage.dangerButtonEventEmmiter);
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.eventEmmiterDanger.unsubscribe()
    this.eventEmmiterConfirm.unsubscribe()
    this.eventEmmiterConfirmYes.unsubscribe()
  }

}
