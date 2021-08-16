import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { CustomersComponent } from '../../pages/customers/customers.component';
import { LoginComponent } from '../../pages/Login/login.component';
import { AuthGuard } from '../../shared/helpers/Guards/auth.guard';
import { AlreadySignedGuard } from '../../shared/helpers/Guards/already-signed.guard';
import { SecurityKeepersComponent } from '../../pages/SecurityKeepers/securityKeepers.component';
import { InvitationsComponent } from '../../pages/invitation/invitations.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent ,canActivate:[AuthGuard]},
    { path: 'customers',           component: CustomersComponent,canActivate:[AuthGuard] },
     { path: 'invitations',           component: InvitationsComponent,canActivate:[AuthGuard] },
     { path: 'security-keepers',   component: SecurityKeepersComponent,canActivate:[AuthGuard] },
     {path:'logout',},
     {path:'login',component:LoginComponent,canActivate:[AlreadySignedGuard]},
];
