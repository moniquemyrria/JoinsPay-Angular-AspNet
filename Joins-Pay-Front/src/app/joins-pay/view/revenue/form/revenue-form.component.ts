import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { RevenueService } from 'src/app/joins-pay/services/revenue/revenue.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IAccount } from '../../register/account/account-model';
import { IDepartment } from '../../register/department/department-model';
import { IRevenueCategory } from '../../register/revenue-category/revenue-category-model';
import { DataIncomes } from '../revenue-model';

@Component({
  selector: 'app-revenue-form',
  templateUrl: './revenue-form.component.html',
  styleUrls: ['./revenue-form.component.scss']
})
export class RevenueFormComponent implements OnInit {

  revenue: DataIncomes = {} as DataIncomes
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  itemsAccount: IAccount[] = [] as IAccount[];
  itemsRevenueCategory: IRevenueCategory[] = [] as IRevenueCategory[];
  itemsDepartment: IDepartment[] = [] as IDepartment[];
  dateCreated = new FormControl(new Date());
  
  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private revenueService: RevenueService,
    private registerService: RegisterService,
    private _snackBar: MatSnackBar
  ) { }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;

    } else {
      this.mobile = false;
    }
  }

  closeAlertMessage() {
    this.displayAlertMessage = false
  }

  openSnackBar(message: string, action: string) {
    let snackBarRef = this._snackBar.open(message, action);
    snackBarRef.afterDismissed().subscribe(() => { });
    snackBarRef.onAction().subscribe(() => { })
  }

  onRouterLinkRevenueList() {
    this.router.navigateByUrl('/joinspay/revenue');
  }

  onSubmit() {
    console.log(this.revenue)
    if (!this.checkValidationFields()) {
      if (this.revenue.id == undefined) {
        this.displayLoading = true
        this.revenue.deleted = "N"
        this.revenue.dateCreated = this.dateCreated.value
        this.revenueService
          .PostRevenue(this.revenue)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Nova Receita",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/revenue' }
              )
            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao inserir a Receita",
                response.message,
                false,
                true,
                undefined,
                true
              )
            }
            this.displayLoading = false
            this.displayAlertMessage = true
          }, (error) => {
            this.alertMesssage = GetAlertMessage(
              "Erro de Conexão",
              "Não foi possível salvar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
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

  }

  initEdit() {
    let routerId: any = this.activeRoute.snapshot.params
    this.revenue.id = routerId.id;

    if (this.revenue.id !== undefined) {
      this.displayLoading = true
      this.revenueService
        .GetIdRevenue(this.revenue.id)
        .subscribe((response: DataIncomes) => {
          this.revenue = response;
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
  }

  checkValidationFields() {
    let schema: ValidationSchema[] = [];

    schema.push(new ValidationSchema("amount", "Valor da Receita", "number", true));
    schema.push(new ValidationSchema("idRevenueCategory", "Categoria", "number", true));
    schema.push(new ValidationSchema("idAccount", "Conta", "number", true));
    schema.push(new ValidationSchema("idDepartment", "Loja / Empresa / Terceiros", "number", true));

    let result = new Validation().ValidSchema(schema, this.revenue);

    if (!result.success) {
      this.openSnackBar(result.message, "Ok")
      return true
    }

    return false
  }

  eventEmmiter(event: Event) {
    console.log(event)
    this.displayAlertMessage = false
  }

  getListAccount() {
    this.displayLoading = true
    this.registerService
      .GetListAccount()
      .subscribe((response: IAccount[]) => {
        this.itemsAccount = response;
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

  getListRevenueCategory() {
    this.displayLoading = true
    this.registerService
      .GetListRevenueCategory()
      .subscribe((response: IRevenueCategory[]) => {
        this.itemsRevenueCategory = response;
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

  getListDepartment() {
    this.displayLoading = true
    this.registerService
      .GetListDepartment()
      .subscribe((response: IDepartment[]) => {
        this.itemsDepartment = response;
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
    this.initEdit()
    this.mobile = CheckMobile()
    this.getListAccount();
    this.getListRevenueCategory()
    this.getListDepartment()
  }

}
