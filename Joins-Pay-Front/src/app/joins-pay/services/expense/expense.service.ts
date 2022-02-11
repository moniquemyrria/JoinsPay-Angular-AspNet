import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { environment } from 'src/environments/environment';
import { IExpense, IExpenseStatus, IExpenseType } from '../../view/expense/expense-model';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json',
		})
	};
  
  constructor(private httpClient: HttpClient) { }

  GetListExpense(): Observable<IExpense[]>  {
    return this.httpClient.get<IExpense[]>(`${environment.baseURL}Expense`, this.httpOptions);
  }

  GetListExpensePeriodDate(dateInitial: string, dateFinal: string): Observable<IExpense[]>  {
    return this.httpClient.get<IExpense[]>(`${environment.baseURL}Expense/PeriodDate/` + dateInitial + '/' + dateFinal, this.httpOptions);
  }

  GetListExpenseStatus(): Observable<IExpenseStatus[]>  {
    return this.httpClient.get<IExpenseStatus[]>(`${environment.baseURL}Expense/ExpenseStatus`, this.httpOptions);
  }

  
  GetListExpenseStatusNew(): Observable<IExpenseStatus[]>  {
    return this.httpClient.get<IExpenseStatus[]>(`${environment.baseURL}Expense/ExpenseStatus/NewExpense`, this.httpOptions);
  }

  GetListExpenseType(): Observable<IExpenseType[]>  {
    return this.httpClient.get<IExpenseType[]>(`${environment.baseURL}Expense/ExpenseType`, this.httpOptions);
  }

  GetIdExpense(id: number): Observable<IExpense>  {
    return this.httpClient.get<IExpense>(`${environment.baseURL}Expense/` + id, this.httpOptions);
  }

  PostExpense(expense: IExpense): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}Expense`, expense, this.httpOptions);
  }

  PutExpense(expense: IExpense): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}Expense/` + expense.id, expense, this.httpOptions);
  }

  DeleteExpense(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}Expense/` + id, this.httpOptions);
  }
}
