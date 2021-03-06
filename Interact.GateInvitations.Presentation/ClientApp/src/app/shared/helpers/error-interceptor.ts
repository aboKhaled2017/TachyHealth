import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http'
import { Observable,throwError } from 'rxjs';
import { AuthService } from '../Services/auth.service';
import { CommonUtilityService } from '../Services/commonUtility.service';
import { catchError } from 'rxjs/operators';
import { ErrorModel } from '../../models/Error.model';
import { ToastService } from '../Services/toast.service.';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private authService:AuthService,
              private uilityService: CommonUtilityService,
              private toastService:ToastService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError((err:HttpErrorResponse)=> {
      
      if ([401, 403].indexOf(err.status) !== -1) {
          // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
          this.authService.logout();
          location.reload(true);
      }
      else if(err.status==500 || err.status==0){
      this.toastService.showError(err.message)
      }
      /* if(err.error.errors || err.error?.status==400){
        return throwError(err.error.errors
          ?this.uilityService.convertObjPropsToCamleCseString(err.error.errors)
          :err);
      }
      if(err.error.status==404){

      } */
      //const error = err.error.message || err.statusText;
      return throwError(new ErrorModel(err,this.uilityService));
  }))
  }
}
