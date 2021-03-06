import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../Services/auth.service';

@Injectable()
export class AlreadySignedGuard implements CanActivate {

  constructor(private authService: AuthService,private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    let isAuthenticated=this.authService.isAuthenticated();
    if(isAuthenticated){
     this.router.navigate(['/dashboard']);
    }
    return true;
  }
}
