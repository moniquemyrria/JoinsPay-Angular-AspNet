import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IExpenseCategory } from '../expense-category-model';

@Component({
  selector: 'app-expense-category-form',
  templateUrl: './expense-category-form.component.html',
  styleUrls: ['./expense-category-form.component.scss']
})
export class ExpenseCategoryFormComponent implements OnInit {

  expenseCategory: IExpenseCategory = {} as IExpenseCategory
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;

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

  closeAlertMessage(){
    this.displayAlertMessage = false
  }


  openSnackBar(message: string, action: string) {
    let snackBarRef = this._snackBar.open(message, action);
    snackBarRef.afterDismissed().subscribe(() => { });
    snackBarRef.onAction().subscribe(() => { })
  }

  onRouterLinkExpenseCategoryList() {
    this.router.navigateByUrl('/joinspay/expensecategory');
  }

  onSubmit() {
    if (!this.checkValidationFields()) {
      if (this.expenseCategory.id == undefined) {
        this.displayLoading = true
        this.expenseCategory.deleted = "N"
        this.expenseCategory.dateCreated = new Date()
        this.registerService
          .PostExpenseCategory(this.expenseCategory)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Nova Categoria",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/expensecategory' }
              )
            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao cadastrar Nova Categoria",
                response.message,
                false,
                true,
                undefined,
                true,
                "Ok"
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
          .PutExpenseCategory(this.expenseCategory)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Alteração de Categoria",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/expensecategory' }
              )
            }else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao Alterar os dados da Categoria",
                response.message,
                false,
                true,
                undefined,
                true,
                "Ok"
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
    this.expenseCategory.id = routerId.id;

    if (this.expenseCategory.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdExpenseCategory(this.expenseCategory.id)
        .subscribe((response: IExpenseCategory) => {
          this.expenseCategory = response;
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

    schema.push(new ValidationSchema("initials", "Sigla", "string", true, 6));
    schema.push(new ValidationSchema("description", "Descrição", "string", true, 30));

    let result = new Validation().ValidSchema(schema, this.expenseCategory);

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


  ngOnInit(): void {
    this.initEdit()
    this.mobile = CheckMobile()
    this.expenseCategory.color = '#1976D2'
  }

}
