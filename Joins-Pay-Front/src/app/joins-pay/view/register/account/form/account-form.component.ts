import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IAccountCategory } from '../../account-category/account-category-model';
import { IDepartment } from '../../department/department-model';
import { IAccount } from '../account-model';

@Component({
  selector: 'app-account-form',
  templateUrl: './account-form.component.html',
  styleUrls: ['./account-form.component.scss']
})
export class AccountFormComponent implements OnInit {

  account: IAccount = {} as IAccount
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  itemsAccountCategory: IAccountCategory[] = [] as IAccountCategory[];
  itemsDepartment: IDepartment[] = [] as IDepartment[];

  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
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

  onRouterLinkAccountList() {
    this.router.navigateByUrl('/joinspay/account');
  }

  onSubmit() {
    console.log(this.account)
    if (!this.checkValidationFields()) {
      if (this.account.id == undefined) {
        this.displayLoading = true
        this.account.deleted = "N"
        this.account.dateCreated = new Date()
        this.registerService
          .PostAccount(this.account)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Nova Conta",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/account' }
              )
            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao cadastrar Nova Conta",
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
              "Não foi possível alterar os dados. Verifique a conexão ou tente novamente em alguns minutos.",
              false,
              true,
              500
            )
            this.displayLoading = false
            this.displayAlertMessage = true
          }
          );
      } else {
        this.registerService
          .PutAccount(this.account)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Alteração de Tipo de Conta",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/account' }
              )
            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao alterar o Tipo de Conta",
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
    this.account.id = routerId.id;

    if (this.account.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdAccount(this.account.id)
        .subscribe((response: IAccount) => {
          this.account = response;
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

    schema.push(new ValidationSchema("code", "Código", "string", true, 10));
    schema.push(new ValidationSchema("name", "Nome", "string", true, 50));
    schema.push(new ValidationSchema("idDepartment", "Empresa", "number", true));
    schema.push(new ValidationSchema("idAccountCategory", "Tipo de Conta", "number", true));
    schema.push(new ValidationSchema("agency", "Agência", "string", true, 10));
    schema.push(new ValidationSchema("accountNumber", "Número da Conta", "string", true, 10));

    let result = new Validation().ValidSchema(schema, this.account);

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

  getListAccountCategory() {
    this.displayLoading = true
    this.registerService
      .GetListAccountCategory()
      .subscribe((response: IAccountCategory[]) => {
        this.itemsAccountCategory = response;
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

  getListDepartment(departmentCategory:  string) {
    this.displayLoading = true
    this.registerService
      .GetListDepartmentForCategory(departmentCategory)
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
    this.getListAccountCategory()
    this.getListDepartment('COMPANY')
  }

}
