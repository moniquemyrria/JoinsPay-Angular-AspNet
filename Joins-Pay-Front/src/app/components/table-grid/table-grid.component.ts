import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { SortEvent } from 'primeng/api';
import { CheckMobile } from 'src/app/check-mobile';


@Component({
  selector: 'app-table-grid',
  templateUrl: './table-grid.component.html',
  styleUrls: ['./table-grid.component.scss']
})
export class TableGridComponent implements OnInit {

  @Output() eventEdit                   = new EventEmitter()
  @Output() eventDelete                 = new EventEmitter()
  @Output() itemsSelected               = new EventEmitter()

  @Input() items              : any[]   = [];
  @Input() cols               : any[]   = [];
  @Input() rowsPerPageOptions : any     = [5,10]
  @Input() rows                         = 5;
  @Input() placeholderSearch            = 'Produtos'
  @Input() buttonsCrud        : boolean = true

  selected: any[] = []

  first = 0;
  mobile: boolean = false;
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;
     
    } else {
      this.mobile = false;
    }
  }

  constructor() {}


  selection(item: any){
    this.itemsSelected.emit(this.selected)
  }

  next() {
    this.first = this.first + this.rows;
  }

  prev() {
    this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.items ? this.first === (this.items.length - this.rows) : true;
  }

  isFirstPage(): boolean {
    return this.items ? this.first === 0 : true;
  }

  deleteItem(item: any){
    this.eventDelete.emit(item)
  }

  editItem(item: any){
    this.eventEdit.emit(item)
  }


  customSort(event: SortEvent) {
    let eventDada: any = event
    let field = eventDada.multiSortMeta[0].field
    let order = eventDada.multiSortMeta[0].order
    let data =  eventDada.data

    data.sort((data1: any, data2: any) => {
        let value1 = data1[field];
        let value2 = data2[field];
        let result = null;

        if (value1 == null && value2 != null)
            result = -1;
        else if (value1 != null && value2 == null)
            result = 1;
        else if (value1 == null && value2 == null)
            result = 0;
        else if (typeof value1 === 'string' && typeof value2 === 'string')
            result = value1.localeCompare(value2);
        else
            result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;

        return (order * result);
    });
}

  ngOnInit(): void {
   this.mobile = CheckMobile()
  }

}
