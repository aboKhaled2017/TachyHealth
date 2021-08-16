import { Component, OnInit } from '@angular/core';
import { ISecurityKeeperViewModel } from './securityKeeper.model';
import { SecurityKeepersService } from './securityKeepers.service';

@Component({
    selector: 'securityKeepers-cmp',
    moduleId: module.id,
    templateUrl: 'securityKeepers.component.html',
    providers:[SecurityKeepersService]
})

export class SecurityKeepersComponent implements OnInit{
    data:ISecurityKeeperViewModel[]=[];
    beingHandledProcesses:string[]=[]
    ngOnInit(){
        this._service.getAllSecurityKeeprs().subscribe(data=>{
            this.data=data;console.log(data);
        })
    }
    constructor(private _service:SecurityKeepersService){

    }
   
   
}
