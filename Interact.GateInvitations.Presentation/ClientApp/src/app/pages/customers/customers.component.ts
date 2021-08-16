import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { ICustomerViewModel } from './customer.model';
import { CustomersService } from './customers.service';

@Component({
    selector: 'customers-cmp',
    moduleId: module.id,
    templateUrl: 'customers.component.html',
    providers:[CustomersService]
})

export class CustomersComponent implements OnInit{
    data:ICustomerViewModel[]=[];
    beingHandledProcesses:string[]=[]
    ngOnInit(){
      this._service.getAllCustomer().subscribe(data => {
        this.data = data.map(d => ({
          ...d,
          attachments:(((d['attachment'] as any as string))
          ?.split(',')||[])
          //.filter(e=>!!e)
          .map(t=>`${environment.base}${t}`) 
        }));
          console.log(this.data.map(d=>d.attachments))
        })
    }
    constructor(private _service:CustomersService){

    }
    isProcessBeingHandled(id:string){
      return this.beingHandledProcesses.some(r=>r==id);
    }
    startToHandleProccess(id:string){
     this.beingHandledProcesses.push(id);
    }
    stopToHandleProccess(id:string){
     this.beingHandledProcesses=this.beingHandledProcesses.filter(r=>r!=id);
    }
    changeStatus(customerId:string){
        this.startToHandleProccess(customerId);
        this._service.changeCustomerStatus(customerId)
        .subscribe(()=>{
           var customer= this.data.find(e=>e.customerId==customerId);
          if(customer?.userStatus){
               customer.userStatus= customer.userStatus=="InActive"?"Active":"InActive";              
          }
          this.stopToHandleProccess(customerId);
        },err=>{
           this.stopToHandleProccess(customerId);
            alert('some error happened')
        });
    }
}
