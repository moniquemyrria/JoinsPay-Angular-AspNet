import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RevenueService } from 'src/app/joins-pay/services/revenue/revenue.service';
import { IRevenue } from '../revenue-model';

@Component({
  selector: 'app-revenue-category-list',
  templateUrl: './revenue-list.component.html',
  styleUrls: ['./revenue-list.component.scss'],
})

export class RevenueListComponent implements OnInit {

  revenue: IRevenue = {} as IRevenue ;
  cols: any = []
  selected: any[] = []
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  idItemDelete: number = 0
  displayCollapsePeriodDate: boolean = false
  displayFilterPeriodDate: boolean = false

  stringPeriodDate: string = ""
  
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;

    } else {
      this.mobile = false;
    }
  }

  openSnackBar(message: string, action: string) {
    let snackBarRef = this._snackBar.open(message, action);
    snackBarRef.afterDismissed().subscribe(() => { });
    snackBarRef.onAction().subscribe(() => { })
  }


  showFilterPeriodDate() {
    this.displayCollapsePeriodDate = !this.displayCollapsePeriodDate
  }

  checkValidationPeriodDate(dates: any) {

    if (dates.dateFinal < dates.dateInicial || dates.inicial > dates.dateInicial){
      this.openSnackBar("O período selecionado está incorreto ou inválido.", "Ok")
      return true
    } 

    return false
  }

  filterPeriodDate(event: any) {
    if (!this.checkValidationPeriodDate(event)) {
      this.displayLoading = true
      this.stringPeriodDate = moment(event.dateInicial).format('DD/MM/YYYY')  + " a " + moment(event.dateFinal).format('DD/MM/YYYY')  
      this.revenueService
        .GetListRevenuePeriodDate(moment(event.dateInicial).format('YYYY-MM-DD'), moment(event.dateFinal).format('YYYY-MM-DD'))
        .subscribe((response: IRevenue) => {
          this.revenue = response;
          this.displayLoading = false
          this.displayCollapsePeriodDate = false
          this.displayFilterPeriodDate = true
        }, (error) => {
          this.alertMesssage = GetAlertMessage(
            "Erro de Conexão",
            "Não foi possível carregar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
            false,
            true,
            500
          )
          this.displayLoading = false
          this.displayAlertMessage = true
        }
        );
    }

  }

  onSelectionRouterLink() {
    this.router.navigateByUrl('/joinspay/revenue/new');
  }

  constructor(
    private revenueService: RevenueService,
    private router: Router,
    private _snackBar: MatSnackBar
  ) { }

  selection(items: any) {
    this.selected = items
    console.log(this.selected)
  }

  editItem(item: any) {
    console.log(item)
    this.router.navigateByUrl('/joinspay/revenue/edit/' + item.id);
  }

  deleteItem(item: any) {
    this.idItemDelete = item.id
    this.alertMesssage = GetAlertMessage(
      "Cancelamento de Receita",
      "Deseja realmente cancelar a Receita selecioanda?",
      false,
      false,
      undefined,
      true,
      'Sim',
      {},
      'Não',
      {}
    )
    this.displayLoading = false
    this.displayAlertMessage = true;
  }

  eventEmmiterConfirmYes() {
    this.deleteRevenue(this.idItemDelete)
  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
    this.getListRevenue()
  }


  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
  }

  deleteRevenue(id: number) {
    this.displayLoading = true
    this.revenueService
      .DeleteRevenue(id)
      .subscribe((response: IContractResponse) => {
        if (response.success) {
          this.alertMesssage = GetAlertMessage(
            "Confirmação de Cancelamento",
            response.message,
            true,
            true,
            undefined,
            false,
            "Ok",
            { routerLink: '/joinspay/revenue' }
          )
          this.displayAlertMessage = true

        }
        this.displayLoading = false
      }, (error) => {
        this.alertMesssage = GetAlertMessage(
          "Erro de Conexão",
          "Não foi possível carregar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
          false,
          true,
          500
        )

      }
      );
  }

  getListRevenue() {
    this.displayLoading = true
    this.revenueService
      .GetListRevenue()
      .subscribe((response: IRevenue) => {
        this.revenue = response;
        this.displayLoading = false
        this.displayCollapsePeriodDate = false
        this.displayFilterPeriodDate = false
      }, (error) => {
        this.alertMesssage = GetAlertMessage(
          "Erro de Conexão",
          "Não foi possível carregar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
          false,
          true,
          500
        )
        this.displayLoading = false
        this.displayAlertMessage = true
      }
      );

  }

  ngOnInit(): void {
    this.mobile = CheckMobile()
    this.displayLoading = true
    this.getListRevenue()

    this.cols = [
      { field: 'color', header: '' },
      { field: 'revenueCategory', header: 'Categoria' },
      { field: 'account', header: 'Conta' },
      { field: 'department', header: 'Empresa, Loja ou Terceiro' },
      { field: 'amountFormatted', header: 'Valor (R$)' },
      { field: 'dateCreatedFormatted', header: 'Data' },
    ];

  }


}