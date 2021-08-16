import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IInvitationViewModel } from './invitation.model';

@Injectable()
export class InvitationsService {

  constructor(private http: HttpClient) { }

  getAllInvitationss(){
    return this.http.get<IInvitationViewModel[]>(`${environment.apiUrl}/admin/invitations`);
  }
  
}
