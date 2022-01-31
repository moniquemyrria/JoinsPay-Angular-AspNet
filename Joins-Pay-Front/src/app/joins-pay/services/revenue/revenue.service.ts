import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IContractResponse } from 'src/app/contract-response/contract-response';
import { environment } from 'src/environments/environment';
import { IRevenue } from '../../view/revenue/revenue-model';

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

  GetListRevenue(): Observable<IRevenue[]>  {
    return this.httpClient.get<IRevenue[]>(`${environment.baseURL}Revenue`, this.httpOptions);
  }

  GetIdRevenue(id: number): Observable<IRevenue>  {
    return this.httpClient.get<IRevenue>(`${environment.baseURL}Revenue/` + id, this.httpOptions);
  }

  PostRevenue(revenue: IRevenue): Observable<IContractResponse>  {
    return this.httpClient.post<IContractResponse>(`${environment.baseURL}Revenue`, revenue, this.httpOptions);
  }

  PutRevenue(revenue: IRevenue): Observable<IContractResponse>  {
    return this.httpClient.put<IContractResponse>(`${environment.baseURL}Revenue/` + revenue.id, revenue, this.httpOptions);
  }

  DeleteRevenue(id: number): Observable<IContractResponse>  {
    return this.httpClient.delete<IContractResponse>(`${environment.baseURL}Revenue/` + id, this.httpOptions);
  }
}
