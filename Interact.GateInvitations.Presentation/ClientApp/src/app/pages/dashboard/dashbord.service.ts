import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IDashbordStatisticData } from './dashbord-statisticts-data.model';

@Injectable()
export class DashbordService {

  constructor(private http: HttpClient) { }

  getAllInvitationss(){
    return this.http.get<IDashbordStatisticData>(`${environment.apiUrl}/admin/summary-data`);
  }
  
}
