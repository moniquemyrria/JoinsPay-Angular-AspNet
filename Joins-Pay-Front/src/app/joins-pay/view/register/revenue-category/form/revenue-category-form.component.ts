import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IRevenueCategory } from '../revenue-category-model';

@Component({
  selector: 'app-revenue-category-form',
  templateUrl: './revenue-category-form.component.html',
  styleUrls: ['./revenue-category-form.component.scss']
})
export class RevenueCategoryFormComponent implements OnInit {

  revenueCategory: IRevenueCategory = {} as IRevenueCategory
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

  onRouterLinkRevenueCategoryList() {
    this.router.navigateByUrl('/joinspay/revenuecategory');
  }

  onSubmit() {
    if (!this.checkValidationFields()) {
      if (this.revenueCategory.id == undefined) {
        this.displayLoading = true
        this.revenueCategory.deleted = "N"
        this.revenueCategory.dateCreated = new Date()
        this.registerService
          .PostRevenueCategory(this.revenueCategory)
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
                { routerLink: '/joinspay/revenuecategory' }
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
          .PutRevenueCategory(this.revenueCategory)
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
                { routerLink: '/joinspay/revenuecategory' }
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
    this.revenueCategory.id = routerId.id;

    if (this.revenueCategory.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdRevenueCategory(this.revenueCategory.id)
        .subscribe((response: IRevenueCategory) => {
          this.revenueCategory = response;
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

    let result = new Validation().ValidSchema(schema, this.revenueCategory);

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
    this.revenueCategory.color = '#1976D2'
  }

}
