import { Component, OnInit } from '@angular/core';
import { IInvitationViewModel } from './invitation.model';
import { InvitationsService } from './invitations.service';

@Component({
    selector: 'invitations-cmp',
    moduleId: module.id,
    templateUrl: 'invitations.component.html',
    providers:[InvitationsService]
})

export class InvitationsComponent implements OnInit{
    data:IInvitationViewModel[]=[];
    ngOnInit(){
        this._service.getAllInvitationss().subscribe(data=>{
            this.data=data.map(r=>({
                             ...r,
                             createdAt:new Date(r.createdAt).toLocaleDateString(),
                             imgUrl:`http://localhost:5000${r.imgUrl}`}))
        })
    }
    constructor(private _service:InvitationsService){

    }
   
   
}
