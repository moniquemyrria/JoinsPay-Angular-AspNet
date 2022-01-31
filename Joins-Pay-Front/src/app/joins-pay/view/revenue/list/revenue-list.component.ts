import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  items: IRevenue[] = [];
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
    this.router.navigateByUrl('/joinspay/revenue/new');
  }

  constructor(
    private revenueService: RevenueService,
    private router: Router,

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
      .subscribe((response: IRevenue[]) => {
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

    this.getListRevenue()

    this.cols = [
      { field: 'id', header: 'Id' },
      { field: 'amountFormatted', header: 'Valor (R$)' },
      { field: 'revenueCategory', header: 'Categoria' },
      { field: 'account', header: 'Conta' },
      { field: 'department', header: 'Empresa, Loja ou Terceiro' },
    ];

  }


}