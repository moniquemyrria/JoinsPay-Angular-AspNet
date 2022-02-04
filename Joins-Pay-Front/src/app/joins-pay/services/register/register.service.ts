import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { environment } from 'src/environments/environment';
import { IAccountCategory } from '../../view/register/account-category/account-category-model';
import { IAccount } from '../../view/register/account/account-model';
import { IDepartmentCategory } from '../../view/register/department/department-category-model';
import { IDepartment } from '../../view/register/department/department-model';
import { IExpenseCategory } from '../../view/register/expense-category/expense-category-model';
import { IPaymentMethodCategory } from '../../view/register/payment-method/payment-method-category-model';
import { IPaymentMethod } from '../../view/register/payment-method/payment-method-model';
import { IRevenueCategory } from '../../view/register/revenue-category/revenue-category-model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json',
		})
	};
  
  constructor(private httpClient: HttpClient) { }

  //revenue category
  GetListRevenueCategory(): Observable<IRevenueCategory[]>  {
    return this.httpClient.get<IRevenueCategory[]>(`${environment.baseURL}RevenueCategory`, this.httpOptions);
  }

  GetIdRevenueCategory(id: number): Observable<IRevenueCategory>  {
    return this.httpClient.get<IRevenueCategory>(`${environment.baseURL}RevenueCategory/` + id, this.httpOptions);
  }

  PostRevenueCategory(revenueCategory: IRevenueCategory): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}RevenueCategory`, revenueCategory, this.httpOptions);
  }

  PutRevenueCategory(revenueCategory: IRevenueCategory): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}RevenueCategory/` + revenueCategory.id, revenueCategory, this.httpOptions);
  }

  DeleteRevenueCategory(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}RevenueCategory/` + id, this.httpOptions);
  }

  //expense category
  GetListExpenseCategory(): Observable<IExpenseCategory[]>  {
    return this.httpClient.get<IExpenseCategory[]>(`${environment.baseURL}ExpenseCategory`, this.httpOptions);
  }

  GetIdExpenseCategory(id: number): Observable<IExpenseCategory>  {
    return this.httpClient.get<IExpenseCategory>(`${environment.baseURL}ExpenseCategory/` + id, this.httpOptions);
  }

  PostExpenseCategory(expenseCategory: IExpenseCategory): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}ExpenseCategory`, expenseCategory, this.httpOptions);
  }

  PutExpenseCategory(expenseCategory: IExpenseCategory): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}ExpenseCategory/` + expenseCategory.id, expenseCategory, this.httpOptions);
  }

  DeleteExpenseCategory(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}ExpenseCategory/` + id, this.httpOptions);
  }

   //account category
   GetListAccountCategory(): Observable<IAccountCategory[]>  {
    return this.httpClient.get<IAccountCategory[]>(`${environment.baseURL}AccountCategory`, this.httpOptions);
  }

  GetIdAccountCategory(id: number): Observable<IAccountCategory>  {
    return this.httpClient.get<IAccountCategory>(`${environment.baseURL}AccountCategory/` + id, this.httpOptions);
  }

  PostAccountCategory(accountCategory: IAccountCategory): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}AccountCategory`, accountCategory, this.httpOptions);
  }

  PutAccountCategory(accountCategory: IAccountCategory): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}AccountCategory/` + accountCategory.id, accountCategory, this.httpOptions);
  }

  DeleteAccountCategory(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}AccountCategory/` + id, this.httpOptions);
  }

  //account
  GetListAccount(): Observable<IAccount[]>  {
    return this.httpClient.get<IAccount[]>(`${environment.baseURL}Account`, this.httpOptions);
  }

  GetIdAccount(id: number): Observable<IAccount>  {
    return this.httpClient.get<IAccount>(`${environment.baseURL}Account/` + id, this.httpOptions);
  }

  PostAccount(account: IAccount): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}Account`, account, this.httpOptions);
  }

  PutAccount(account: IAccount): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}Account/` + account.id, account, this.httpOptions);
  }

  DeleteAccount(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}Account/` + id, this.httpOptions);
  }

   //department
   GetListDepartmentForCategory(departmentCategory: string): Observable<IDepartment[]>  {

    let httpOptionsGetListDepartment = {
      headers: new HttpHeaders({
        'Content-Type'                : 'application/json',
        'departmentCategory'          : departmentCategory,
      })
    };

    return this.httpClient.get<IDepartment[]>(`${environment.baseURL}Department/`, httpOptionsGetListDepartment);
  }

  GetListDepartment(): Observable<IDepartment[]>  {

    return this.httpClient.get<IDepartment[]>(`${environment.baseURL}Department/Departments`, this.httpOptions);
  }

  GetIdDepartment(id: number): Observable<IDepartment>  {
    return this.httpClient.get<IDepartment>(`${environment.baseURL}Department/` + id, this.httpOptions);
  }

  PostDepartment(department: IDepartment): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}Department`, department, this.httpOptions);
  }

  PutDepartment(department: IDepartment): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}Department/` + department.id, department, this.httpOptions);
  }

  DeleteDepartment(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}Department/` + id, this.httpOptions);
  }

  //payment method
  GetListPaymentMethod(): Observable<IPaymentMethod[]>  {
    return this.httpClient.get<IPaymentMethod[]>(`${environment.baseURL}PaymentMethod`, this.httpOptions);
  }

  GetListPaymentMethodCategory(): Observable<IPaymentMethodCategory[]>  {
    return this.httpClient.get<IPaymentMethodCategory[]>(`${environment.baseURL}PaymentMethod/PaymentMethodCategory`, this.httpOptions);
  }
  
  GetIdPaymentMethod(id: number): Observable<IPaymentMethod>  {
    return this.httpClient.get<IPaymentMethod>(`${environment.baseURL}PaymentMethod/` + id, this.httpOptions);
  }

  PostPaymentMethod(paymentMethod: IPaymentMethod): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}PaymentMethod`, paymentMethod, this.httpOptions);
  }

  PutPaymentMethod(paymentMethod: IPaymentMethod): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}PaymentMethod/` + paymentMethod.id, paymentMethod, this.httpOptions);
  }

  DeletePaymentMethod(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}PaymentMethod/` + id, this.httpOptions);
  }

  DeletePaymentMethodCategory(idPaymentMethod: number, idPaymentMethodCategory: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}PaymentMethod/PaymentMethodCategory/` + idPaymentMethod + "/" + idPaymentMethodCategory, this.httpOptions);
  }

}
