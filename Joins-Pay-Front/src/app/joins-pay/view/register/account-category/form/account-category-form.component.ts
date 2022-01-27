import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IAccountCategory } from '../account-category-model';

@Component({
  selector: 'app-account-category-form',
  templateUrl: './account-category-form.component.html',
  styleUrls: ['./account-category-form.component.scss']
})
export class AccountCategoryFormComponent implements OnInit {

  accountCategory: IAccountCategory = {} as IAccountCategory
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

  openSnackBar(message: string, action: string) {
    let snackBarRef = this._snackBar.open(message, action);
    snackBarRef.afterDismissed().subscribe(() => { });
    snackBarRef.onAction().subscribe(() => { })
  }

  onRouterLinkAccountCategoryList() {
    this.router.navigateByUrl('/joinspay/accountcategory');
  }

  onSubmit() {
    if (!this.checkValidationFields()) {
      if (this.accountCategory.id == undefined) {
        this.displayLoading = true
        this.accountCategory.deleted = "N"
        this.accountCategory.standard = "N"
        this.accountCategory.dateCreated = new Date()
        this.registerService
          .PostAccountCategory(this.accountCategory)
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
                { routerLink: '/joinspay/accountcategory' }
              )
              this.displayLoading = false
              this.displayAlertMessage = true
            }

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
          .PutAccountCategory(this.accountCategory)
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
                { routerLink: '/joinspay/accountcategory' }
              )
              this.displayLoading = false
              this.displayAlertMessage = true
            }

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
    this.accountCategory.id = routerId.id;

    if (this.accountCategory.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdAccountCategory(this.accountCategory.id)
        .subscribe((response: IAccountCategory) => {
          this.accountCategory = response;
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

    let result = new Validation().ValidSchema(schema, this.accountCategory);

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
  }

}
