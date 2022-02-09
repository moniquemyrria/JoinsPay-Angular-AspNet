import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { ExpenseService } from 'src/app/joins-pay/services/expense/expense.service';
import { IExpense, IExpenseType } from '../expense-model';

@Component({
  selector: 'app-expense-category-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.scss'],
})

export class ExpenseListComponent implements OnInit {

  expense: IExpense[] = [] as IExpense[];
  cols: any = []
  selected: any[] = []
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  idItemDelete: number = 0
  displayCollapsePeriodDate: boolean = false
  displayFilterPeriodDate: boolean = false
  itemsExpenseTypes: any[] = [];

  stringPeriodDate: string = ""

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;

    } else {
      this.mobile = false;
    }
  }

  teste() { }

  openSnackBar(message: string, action: string) {
    let snackBarRef = this._snackBar.open(message, action);
    snackBarRef.afterDismissed().subscribe(() => { });
    snackBarRef.onAction().subscribe(() => { })
  }


  showFilterPeriodDate() {
    this.displayCollapsePeriodDate = !this.displayCollapsePeriodDate
  }

  checkValidationPeriodDate(dates: any) {

    if (dates.dateFinal < dates.dateInicial || dates.inicial > dates.dateInicial) {
      this.openSnackBar("O período selecionado está incorreto ou inválido.", "Ok")
      return true
    }

    return false
  }

  // filterPeriodDate(event: any) {
  //   if (!this.checkValidationPeriodDate(event)) {
  //     this.displayLoading = true
  //     this.stringPeriodDate = moment(event.dateInicial).format('DD/MM/YYYY')  + " a " + moment(event.dateFinal).format('DD/MM/YYYY')  
  //     this.expenseService
  //       .GetListRevenuePeriodDate(moment(event.dateInicial).format('YYYY-MM-DD'), moment(event.dateFinal).format('YYYY-MM-DD'))
  //       .subscribe((response: IExpense) => {
  //         this.expense = response;
  //         this.displayLoading = false
  //         this.displayCollapsePeriodDate = false
  //         this.displayFilterPeriodDate = true
  //       }, (error) => {
  //         this.alertMesssage = GetAlertMessage(
  //           "Erro de Conexão",
  //           "Não foi possível carregar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
  //           false,
  //           true,
  //           500
  //         )
  //         this.displayLoading = false
  //         this.displayAlertMessage = true
  //       }
  //       );
  //   }

  // }

  onSelectionRouterLink() {
    this.router.navigateByUrl('/joinspay/expense/new');
  }

  constructor(
    private expenseService: ExpenseService,
    private router: Router,
    private _snackBar: MatSnackBar
  ) { }

  selection(items: any) {
    this.selected = items
    console.log(this.selected)
  }

  editItem(item: any) {
    console.log(item)
    this.router.navigateByUrl('/joinspay/expense/edit/' + item.id);
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
    this.deleteExpense(this.idItemDelete)
  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
    this.getListExpense()
  }


  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
  }

  deleteExpense(id: number) {
    this.displayLoading = true
    this.expenseService
      .DeleteExpense(id)
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
            { routerLink: '/joinspay/expense' }
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

  getListExpense() {
    this.displayLoading = true
    this.expenseService
      .GetListExpense()
      .subscribe((response: IExpense[]) => {
        this.expense = response;
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

  getListExpenseTypes() {
    this.displayLoading = true
    this.expenseService
      .GetListExpenseType()
      .subscribe((response: IExpenseType[]) => {

        response.forEach((item: IExpenseType) => {

          let itemExpenseType = {
            label: item.description, icon: item.icon, routerLink: "/joinspay/" + item.routerLink + "/new",
          }

          this.itemsExpenseTypes.push(itemExpenseType);
        });

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
    this.getListExpense()

    this.cols = [
      { field: 'color', header: '' },
      { field: 'revenueCategory', header: 'Categoria' },
      { field: 'account', header: 'Conta' },
      { field: 'department', header: 'Empresa, Loja ou Terceiro' },
      { field: 'amountFormatted', header: 'Valor (R$)' },
      { field: 'dateCreatedFormatted', header: 'Data' },
    ];

    this.getListExpenseTypes()

  }


}