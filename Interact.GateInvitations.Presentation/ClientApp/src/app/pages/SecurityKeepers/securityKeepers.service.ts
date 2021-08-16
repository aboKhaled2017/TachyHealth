import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ISecurityKeeperViewModel } from './securityKeeper.model';

@Injectable()
export class SecurityKeepersService {

  constructor(private http: HttpClient) { }

  getAllSecurityKeeprs(){
    return this.http.get<ISecurityKeeperViewModel[]>(`${environment.apiUrl}/admin/securitykeepers`);
  }
  
}
