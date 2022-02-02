import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CheckMobile } from 'src/app/check-mobile';
import { GetAlertMessage } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message';
import { AlertMessageModel } from 'src/app/components/modal/modal-alerts-message/modal-alerts-message-model';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { RegisterService } from 'src/app/joins-pay/services/register/register.service';
import { IDepartment } from '../department-model';


@Component({
  selector: 'app-expense-category-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.scss'],
})

export class DepartmentListComponent implements OnInit {

  items: IDepartment[] = [];
  cols: any[] = []
  selected: any[] = []
  alertMesssage: AlertMessageModel = {} as AlertMessageModel
  mobile: boolean = false;
  displayLoading: boolean = false
  displayAlertMessage: boolean = false;
  idItemDelete: number = 0
  descriptionTitle: string = ""
  titleButtonNew: string = ""
  routerName: string = ""
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (event.target.innerWidth <= 768) {
      this.mobile = true;

    } else {
      this.mobile = false;
    }
  }

  onSelectionRouterLink() {
    this.router.navigateByUrl('/joinspay/department/' + this.routerName + '/new');
  }

  constructor(
    private registerService: RegisterService,
    private router: Router,
    private activeRoute: ActivatedRoute,

  ) { }

  selection(items: any) {
    this.selected = items
    console.log(this.selected)
  }

  editItem(item: any) {
    this.router.navigateByUrl("/joinspay/department/" +  this.routerName + "/edit/" + item.id);
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
    this.deleteDepartment(this.idItemDelete)
  }

  eventEmmiterConfirm() {
    this.displayAlertMessage = false
    this.getListDepartment(this.routerName)
  }


  eventEmmiterNotConfirm() {
    this.displayLoading = false
    this.displayAlertMessage = false;
  }

  deleteDepartment(id: number) {
    this.displayLoading = true
    this.registerService
      .DeleteDepartment(id)
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
            { routerLink: '/joinspay/Department' }
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

  getListDepartment(departmentCategory:  string) {
    this.displayLoading = true
    this.registerService
      .GetListDepartmentForCategory(departmentCategory)
      .subscribe((response: IDepartment[]) => {
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

  checkRouterName(routerName?: string) {
    
    switch (routerName) {
      case 'store':
        this.descriptionTitle = "Lojas";
        this.titleButtonNew = "Nova Loja";
        break;
      case 'company':
        this.descriptionTitle = "Empresas";
        this.titleButtonNew = "Nova Empresa";
        break;
      case 'people':
        this.descriptionTitle = "Terceiros (Pessoa Física)";
        this.titleButtonNew = "Nova Pessoa Física";
        break;
    }
  }

  ngOnInit(): void {
    this.mobile = CheckMobile()

    let routerName: any = this.activeRoute.routeConfig?.path
    this.routerName = routerName
    this.checkRouterName(this.routerName)

    this.getListDepartment(this.routerName);

    this.cols = [
      { field: 'id', header: 'Id' },
      { field: 'name', header: 'Nome' },
    ];

  }


}