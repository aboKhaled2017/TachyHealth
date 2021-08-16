import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AdminLayoutRoutes } from './admin-layout.routing';

import { DashboardComponent }       from '../../pages/dashboard/dashboard.component';
import { CustomersComponent }            from '../../pages/customers/customers.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from '../../pages/Login/login.component';
import { SharedModule } from './../../shared/shared.module';
import { SecurityKeepersComponent } from '../../pages/SecurityKeepers/securityKeepers.component';
import { InvitationsComponent } from '../../pages/invitation/invitations.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  declarations: [
    DashboardComponent,
    CustomersComponent,
    SecurityKeepersComponent,
    LoginComponent,
    InvitationsComponent
  ],
  providers:[]
})

export class AdminLayoutModule {}
