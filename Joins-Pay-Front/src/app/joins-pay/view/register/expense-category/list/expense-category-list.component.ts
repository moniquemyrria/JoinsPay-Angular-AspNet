import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { IExpenseCategory } from '../expense-category-model';


@Component({
  selector: 'app-expense-category-list',
  templateUrl: './expense-category-list.component.html',
  styleUrls: ['./expense-category-list.component.scss'],
})

export class ExpenseCategoryListComponent implements OnInit {

  items: IExpenseCategory[] = [];
  cols: any[] = []
  selected: any[] = []
  alertMesssage: AlertMessageModel = {} as AlertMessageModel

  mobile: boolean = false;

  displayLoading: boolean = false

  displayAlertMessage: boolean = false;

  idItemDelete: number = 0

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;

    } else {
      this.mobile = false;
    }
  }



  onSelectionRouterLink() {
    this.router.navigateByUrl('/joinspay/expensecategory/new');
  }

  constructor(
    private registerService: RegisterService,
    private router: Router,

  ) { }

  selection(items: any) {
    this.selected = items
    console.log(this.selected)
  }

  editItem(item: any) {
    console.log(item)
    this.router.navigateByUrl('/joinspay/expensecategory/edit/' + item.id);
  }

  deleteItem(item: any) {
    this.idItemDelete = item.id
    this.alertMesssage = GetAlertMessage(
      "Exclusão de Categoria",
      "Deseja realmente excluír a receita selecioanda?",
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
    this.deleteExpenseCategory(this.idItemDelete)
  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
    this.getListExpenseCategory()
  }


  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
  }

  deleteExpenseCategory(id: number) {
    this.displayLoading = true
    this.registerService
      .DeleteExpenseCategory(id)
      .subscribe((response: IContractResponse) => {
        if (response.success) {
          this.alertMesssage = GetAlertMessage(
            "Confirmação de Exclusão",
            response.message,
            true,
            true,
            undefined,
            false,
            "Ok",
            { routerLink: '/joinspay/expensecategory' }
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

  getListExpenseCategory() {
    this.displayLoading = true
    this.registerService
      .GetListExpenseCategory()
      .subscribe((response: IExpenseCategory[]) => {
        this.items = response;
        this.displayLoading = false
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

    this.getListExpenseCategory()

    this.cols = [
      { field: 'id', header: 'Id' },
      { field: 'initials', header: 'Sigla' },
      { field: 'description', header: 'Descrição' },
      { field: 'color', header: 'Cor' }
    ];

  }


}