import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { ExpenseService } from 'src/app/joins-pay/services/expense/expense.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IExpense, IExpenseStatus } from '../../expense-model';
import { IAccount } from '../../../register/account/account-model';
import { IExpenseCategory } from '../../../register/expense-category/expense-category-model';
import { IDepartment } from '../../../register/department/department-model';
import { IPaymentMethod } from '../../../register/payment-method/payment-method-model';
import { IPaymentMethodCategory } from '../../../register/payment-method/payment-method-category-model';

@Component({
  selector: 'app-expense-fixed-form',
  templateUrl: './expense-fixed-form.component.html',
  styleUrls: ['./expense-fixed-form.component.scss']
})
export class ExpenseFixedFormComponent implements OnInit {

  expenseFixed: IExpense = {} as IExpense
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  displayDatePayment: boolean = false;
  itemsAccount: IAccount[] = [] as IAccount[];
  itemsExpenseCategory: IExpenseCategory[] = [] as IExpenseCategory[];
  itemsDepartment: IDepartment[] = [] as IDepartment[];
  itemsExpenseStatus: IExpenseStatus[] = [] as IExpenseStatus[]
  itemsPaymentMethod: IPaymentMethod[] = [] as IPaymentMethod[]
  itemsPaymentMethodCategory: IPaymentMethodCategory[] = [] as IPaymentMethodCategory[]
  dateCreated = new FormControl(new Date());
  datePayment = new FormControl(new Date());

  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private expenseService: ExpenseService,
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

  onRouterLinkExpenseList() {
    this.router.navigateByUrl('/joinspay/expense');
  }

  onSubmit() {
    console.log(this.expenseFixed)
    if (!this.checkValidationFields()) {
      this.displayLoading = true
      this.expenseFixed.dateCreated = this.dateCreated.value
      if (this.displayDatePayment) {
        this.expenseFixed.paymentDate = this.datePayment.value
      }
      this.expenseFixed.paymentMethodCategory = this.itemsPaymentMethodCategory.filter((t: any) => t.amountCategory !== null)
      this.expenseService
        .PostExpense(this.expenseFixed)
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
              { routerLink: '/joinspay/expense' }
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

  initEdit() {
    let routerId: any = this.activeRoute.snapshot.params
    this.expenseFixed.id = routerId.id;

    if (this.expenseFixed.id !== undefined) {
      this.displayLoading = true
      this.expenseService
        .GetIdExpense(this.expenseFixed.id)
        .subscribe((response: IExpense) => {
          this.expenseFixed = response;
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
    } else {
      this.expenseFixed.idExpenseStatus = 1
    }
  }

  checkValidationFields() {
    let schema: ValidationSchema[] = [];

    schema.push(new ValidationSchema("idExpenseCategory", "Categoria", "number", true));
    schema.push(new ValidationSchema("idAccount", "Conta", "number", true));
    schema.push(new ValidationSchema("idDepartment", "Loja / Empresa / Terceiros", "number", true));
    schema.push(new ValidationSchema("idPaymentMethod", "Forma de Pagamento", "number", true));

    let result = new Validation().ValidSchema(schema, this.expenseFixed);

    if (!result.success) {
      this.openSnackBar(result.message, "Ok")
      return true
    }

    let paymentMethodCategory = this.itemsPaymentMethodCategory.filter((t: any) => t.amountCategory !== null)
    if (paymentMethodCategory.length == 0){
      this.openSnackBar("Informe o valor ao menos a uma das Condições de Pagamento", "Ok")
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

  getListExpenseCategory() {
    this.displayLoading = true
    this.registerService
      .GetListExpenseCategory()
      .subscribe((response: IExpenseCategory[]) => {
        this.itemsExpenseCategory = response;
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

  getListPaymentMethod() {
    this.displayLoading = true
    this.registerService
      .GetListPaymentMethod()
      .subscribe((response: IPaymentMethod[]) => {

        this.itemsPaymentMethod = response
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

  getListExpenseStatusNew() {
    this.displayLoading = true
    this.expenseService
      .GetListExpenseStatusNew()
      .subscribe((response: IExpenseStatus[]) => {
        this.itemsExpenseStatus = response;
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

  changePaymentMethodCategory(idPaymentMethod: number) {

    let paymentMethod: any = this.itemsPaymentMethod.find(
      (item: any) => item.id == idPaymentMethod
    );

    if (paymentMethod != null) {
      this.itemsPaymentMethodCategory = paymentMethod.paymentMethodCategory
    }

  }

  changeSumAmountPaymentMethodCategory() {
    let sum = 0
    this.itemsPaymentMethodCategory.forEach((item: IPaymentMethodCategory) => {
      if (item.amountCategory != null) {
        sum = sum + item.amountCategory
      }
    });
    this.expenseFixed.amount = sum;
  }

  changeShowDatePayment(item: any) {
    let itemExpenseStatus = this.itemsExpenseStatus.find((t: IExpenseStatus) => t.id == item)

    this.displayDatePayment = itemExpenseStatus?.description == 'PAGO' ? true : false;
  }


  ngOnInit(): void {
    this.initEdit()
    this.mobile = CheckMobile()
    this.getListAccount();
    this.getListExpenseCategory()
    this.getListDepartment()
    this.getListExpenseStatusNew()
    this.getListPaymentMethod();
  }

}
