import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { IAccount } from '../account-model';


@Component({
  selector: 'app-revenue-category-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss'],
})

export class AccountListComponent implements OnInit {

  items: IAccount[] = [];
  cols: any = []
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
    this.router.navigateByUrl('/joinspay/account/new');
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
    this.router.navigateByUrl('/joinspay/account/edit/' + item.id);
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
    this.deleteAccount(this.idItemDelete)
  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
    this.getListAccount()
  }


  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
  }

  deleteAccount(id: number) {
    this.displayLoading = true
    this.registerService
      .DeleteAccount(id)
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
            { routerLink: '/joinspay/Account' }
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

  getListAccount() {
    this.displayLoading = true
    this.registerService
      .GetListAccount()
      .subscribe((response: IAccount[]) => {
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

    this.getListAccount()

    this.cols = [
      { field: 'code', header: 'Código' },
      { field: 'name', header: 'Nome' },
      { field: 'agency', header: 'Agência' },
      { field: 'accountNumber', header: 'Conta' },
      { field: 'accountCategory', header: 'Tipo de Conta' },
      { field: 'department', header: 'Empresa' },
    ];

  }


}