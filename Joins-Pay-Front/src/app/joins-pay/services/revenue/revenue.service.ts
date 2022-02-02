import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { environment } from 'src/environments/environment';
import { DataIncomes, IRevenue } from '../../view/revenue/revenue-model';

@Injectable({
  providedIn: 'root'
})
export class RevenueService {
  httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json',
		})
	};
  
  constructor(private httpClient: HttpClient) { }

  GetListRevenue(): Observable<IRevenue>  {
    return this.httpClient.get<IRevenue>(`${environment.baseURL}Revenue`, this.httpOptions);
  }

  GetListRevenuePeriodDate(dateInitial: string, dateFinal: string): Observable<IRevenue>  {
    return this.httpClient.get<IRevenue>(`${environment.baseURL}Revenue/PeriodDate/` + dateInitial + '/' + dateFinal, this.httpOptions);
  }

  GetIdRevenue(id: number): Observable<DataIncomes>  {
    return this.httpClient.get<DataIncomes>(`${environment.baseURL}Revenue/` + id, this.httpOptions);
  }

  PostRevenue(revenue: DataIncomes): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}Revenue`, revenue, this.httpOptions);
  }

  PutRevenue(revenue: DataIncomes): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}Revenue/` + revenue.id, revenue, this.httpOptions);
  }

  DeleteRevenue(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}Revenue/` + id, this.httpOptions);
  }
}
