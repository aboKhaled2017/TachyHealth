import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/Services/auth.service';
import { IDashbordStatisticData } from './dashbord-statisticts-data.model';
import { DashbordService } from './dashbord.service';


@Component({
    selector: 'dashboard-cmp',
    moduleId: module.id,
    templateUrl: 'dashboard.component.html',
    providers:[DashbordService]
})

export class DashboardComponent implements OnInit{

    constructor(private _service:DashbordService,authService:AuthService){
        if(authService.isAuthenticated()){
            _service.getAllInvitationss().subscribe(data=>this.staticData=data)
        }
    }
    staticData:IDashbordStatisticData={
        customersCount:0,
        invitationsCount:0,
        loginTriesCount:0,
        securityKeepersCount:0
    };
    ngOnInit(){
     
    }
}
