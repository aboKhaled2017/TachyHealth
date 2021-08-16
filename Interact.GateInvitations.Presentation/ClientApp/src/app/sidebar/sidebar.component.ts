import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/Services/auth.service';


export interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

export const ROUTES: RouteInfo[] = [
    { path: '/dashboard',     title: 'Dashboard',         icon:'',       class: '' },
    { path: '/customers',         title: 'Customers',             icon:'',    class: '' },
    { path: '/security-keepers',          title: 'Security Keepers',              icon:'',      class: '' },
    { path: '/invitations', title: 'Invitations',     icon:'',    class: '' },
    { path: '/logout', title: 'Logout',     icon:'',    class: '' },
    
];

@Component({
    moduleId: module.id,
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit {
    public menuItems: any[];
    constructor(public authService:AuthService){

    }
    ngOnInit() {
        this.menuItems = ROUTES.filter(menuItem => true);      
    }
    logout(){
        this.authService.logout()
    }
}
