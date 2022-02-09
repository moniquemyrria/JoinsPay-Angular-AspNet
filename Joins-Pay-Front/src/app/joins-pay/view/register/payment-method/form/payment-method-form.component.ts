import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IPaymentMethodCategory } from '../../payment-method/payment-method-category-model';
import { IPaymentMethod } from '../payment-method-model';
import { IAccount } from '../../account/account-model';

@Component({
  selector: 'app-payment-method-form',
  templateUrl: './payment-method-form.component.html',
  styleUrls: ['./payment-method-form.component.scss']
})
export class PaymentMethodFormComponent implements OnInit {

  paymentMethod: IPaymentMethod = {} as IPaymentMethod
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  itemsPaymentMethodCategory: IPaymentMethodCategory[] = [] as IPaymentMethodCategory[];
  itemsAccount: IAccount[] = [] as IAccount[];
  itemSelectedPaymentMethodCategory: any;
  idItemDelete: number = 0
  cols: any[] = []
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

  eventEmmiterConfirmYes(event: any) {
    if (event.delete) {
      this.removeItemPaymentMethodCategory(this.idItemDelete)
    }

    if (event.closeDisplayAlertMessage) {
      this.displayAlertMessage = false
    }

  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
  }

  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
    this.idItemDelete
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
      { delete: true },
      'Não',
      {}
    )
    this.displayLoading = false
    this.displayAlertMessage = true;
  }

  checkFindPaymentMethodCategoryInsert(paymentMethodCategory: IPaymentMethodCategory) {

    let paymentMethodCategoryResult = this.paymentMethod.paymentMethodCategory.find((item: any) => item == paymentMethodCategory)

    return paymentMethodCategoryResult == null ? false : true
  }

  addPaymentMethod() {
    
    if (this.itemSelectedPaymentMethodCategory == undefined) {
      this.openSnackBar("Selecione um Tipo de Pagamento para inserir a condição.", "Ok")
      return
    }

    if (!this.checkFindPaymentMethodCategoryInsert(this.itemSelectedPaymentMethodCategory)) {
      this.paymentMethod.paymentMethodCategory.push(this.itemSelectedPaymentMethodCategory)
      this.itemSelectedPaymentMethodCategory = undefined
    } else {
      this.openSnackBar("Tipo de Pagamento já inserido.", "Ok")
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

  onRouterLinkPaymentMethodList() {
    this.router.navigateByUrl('/joinspay/paymentmethod');
  }

  onSubmit() {

    if (!this.checkValidationFields()) {
      if (this.paymentMethod.id == undefined) {
        this.displayLoading = true
        this.paymentMethod.deleted = "N"
        this.paymentMethod.dateCreated = new Date()
        this.registerService
          .PostPaymentMethod(this.paymentMethod)
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
                { routerLink: '/joinspay/paymentmethod' }
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
          .PutPaymentMethod(this.paymentMethod)
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
                { routerLink: '/joinspay/paymentmethod' }
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
    this.paymentMethod.id = routerId.id;

    if (this.paymentMethod.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdPaymentMethod(this.paymentMethod.id)
        .subscribe((response: IPaymentMethod) => {
          this.paymentMethod = response;
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

  removeItemPaymentMethodCategory(id: number) {

    if (id != null) {
      let paymentMethodCategory: any = this.paymentMethod.paymentMethodCategory.find((t: any) => t.id == id)

      let indexOf: number = this.paymentMethod.paymentMethodCategory.indexOf(paymentMethodCategory);

      this.paymentMethod.paymentMethodCategory.splice(indexOf, 1)

      this.displayAlertMessage = false
    }

  }


  checkValidationFields() {
    let schema: ValidationSchema[] = [];

    if (this.paymentMethod.acceptInstallment == null) {
      this.openSnackBar("É necessário informar se aceita parcelamento, (Sim) ou (Não)", "Ok")
      return true
    }

    schema.push(new ValidationSchema("name", "Nome", "string", true, 30));
    schema.push(new ValidationSchema("idAccount", "Conta", "number", true));

    let result = new Validation().ValidSchema(schema, this.paymentMethod);

    if (!result.success) {
      this.openSnackBar(result.message, "Ok")
      return true
    }

    if (this.paymentMethod.paymentMethodCategory.length == 0) {
      this.openSnackBar("É necessário inserir ao menos uma Condição de Pagamento.", "Ok")
      return true
    }

    return false
  }

  eventEmmiter(event: Event) {
    console.log(event)
    this.displayAlertMessage = false
  }

  getListPaymentMethodCategory() {
    this.displayLoading = true
    this.registerService
      .GetListPaymentMethodCategory()
      .subscribe((response: IPaymentMethodCategory[]) => {
        this.itemsPaymentMethodCategory = response;
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

  ngOnInit(): void {
    this.initEdit()
    this.mobile = CheckMobile()
    this.getListPaymentMethodCategory()
    this.getListAccount()

    this.cols = [
      { field: 'description', header: 'Descrição' },

    ];

    this.paymentMethod.paymentMethodCategory = []
  }

}
