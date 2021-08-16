import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ICustomerViewModel } from './customer.model';

@Injectable()
export class CustomersService {

  constructor(private http: HttpClient) { }

  getAllCustomer(){
    return this.http.get<ICustomerViewModel[]>(`${environment.apiUrl}/admin/customers`);
  }
  changeCustomerStatus(customerId:string){
   return this.http.put(`${environment.apiUrl}/admin/set-customer-status/${customerId}`,null);
  }
}
