import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { environment } from 'src/environments/environment';
import { IExpenseCategory } from '../../view/register/expense-category/expense-category-model';
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

  //

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
}
