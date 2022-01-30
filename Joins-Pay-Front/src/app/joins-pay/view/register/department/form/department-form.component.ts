import { Component, HostListener, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { Validation, ValidationSchema } from 'src/app/validation-fields/validation';
import { IDepartment } from '../department-model';

@Component({
  selector: 'app-expense-category-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.scss']
})
export class DepartmentFormComponent implements OnInit {

  department: IDepartment = {} as IDepartment
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  descriptionTitle: string = ""
  descriptionSubTitle: string = ""
  routerName: string | undefined = ""

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

  onRouterLinkDepartmentList() {
    this.router.navigateByUrl('/joinspay/department/' + this.routerName);
  }

  onSubmit() {
    if (!this.checkValidationFields()) {
      if (this.department.id == undefined) {
        this.displayLoading = true
        this.department.deleted = "N"
        this.department.departmentCategory = this.routerName
        this.department.dateCreated = new Date()
        this.registerService
          .PostDepartment(this.department)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Novo Cadastro",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/department/' + this.routerName }
              )

            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao Alterar o Cadastro",
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
          .PutDepartment(this.department)
          .subscribe((response: IContractResponse) => {
            if (response.success) {
              this.alertMesssage = GetAlertMessage(
                "Alteração de Cadastro",
                response.message,
                true,
                true,
                undefined,
                false,
                "Ok",
                { routerLink: '/joinspay/department/' + this.routerName }
              )
            } else {
              this.alertMesssage = GetAlertMessage(
                "Erro ao Alterar o Cadastro",
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

  closeAlertMessage(){
    this.displayAlertMessage = false
  }

  initEdit() {
    let routerId: any = this.activeRoute.snapshot.params
    this.department.id = routerId.id;

    this.checkRouterName(this.department.id, this.routerName)

    if (this.department.id !== undefined) {
      this.displayLoading = true
      this.registerService
        .GetIdDepartment(this.department.id)
        .subscribe((response: IDepartment) => {
          this.department = response;
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

    schema.push(new ValidationSchema("name", "Nome", "string", true, 50));

    let result = new Validation().ValidSchema(schema, this.department);

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

  checkRouterName(id?: number, routerName?: string) {

    switch (this.routerName) {
      case 'store':
        this.descriptionTitle = "Loja";
        this.descriptionSubTitle = id !== undefined ? "Edição de dados da Loja" : "Cadastro de Nova Loja";
        break;
      case 'company':
        this.descriptionTitle = "Empresa";
        this.descriptionSubTitle = id !== undefined ? "Edição de dados da Empresa" : "Cadastro de Nova Empresa";
        break;
      case 'people':
        this.descriptionTitle = "Terceiros (Pessoa Física)";
        this.descriptionSubTitle = id !== undefined ? "Edição de dados da Pessoa Física" : "Cadastro de Nova Pessoa Física";
        break;
    }
  }


  ngOnInit(): void {
    this.routerName = this.activeRoute.routeConfig?.path?.split("/", 1)[0]
    this.initEdit()
    this.mobile = CheckMobile()
  }

}
