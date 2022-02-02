import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-period-date',
  templateUrl: './period-date.component.html',
  styleUrls: ['./period-date.component.scss']
})
export class PeriodDateComponent implements OnInit {

  dInitial = new FormControl(new Date());
  dFinal = new FormControl(new Date());

  @Output() periodDate = new EventEmitter()
  
  filter(){
    this.periodDate.emit({dateInicial: this.dInitial.value, dateFinal: this.dFinal.value})
  }

  constructor() { }

  ngOnInit(): void {
  }

}
