import { Component, OnInit } from '@angular/core';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';

@Component({
  selector: 'app-joins-pay',
  templateUrl: './joins-pay.component.html',
  styleUrls: ['./joins-pay.component.scss'],
})
export class JoinsPayComponent implements OnInit {

  displayAlertMessage: boolean = false;
  alertMesssage: AlertMessageModel = {} as AlertMessageModel

  date1: Date = new Date();


  constructor() { }

  display: boolean = false;

  showDialog() {
    this.display = true;
  }

  eventEmmiter(event: Event){
    console.log(event)
    this.displayAlertMessage = false
  }

  showAlertMensage(){

    this.alertMesssage = GetAlertMessage(
      "Teste Titulo",
      "Texto do body",
      false,
      false,
      undefined,
      true,
      "Sim",
      {text: "Teste objeto"},
      "Não",
      {text: "Teste objeto Não"},
    )

    this.displayAlertMessage = true

  }

  ngOnInit(): void {
  }

}
