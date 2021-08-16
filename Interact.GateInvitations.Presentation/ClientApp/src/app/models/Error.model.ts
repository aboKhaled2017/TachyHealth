import { HttpErrorResponse } from "@angular/common/http";
import { CommonUtilityService } from "../shared/Services/commonUtility.service";

export interface IErrorModel{
    status:ErrorStatus,
    error?:{[key:string]:any},
    message?:string
    hasValidationError:boolean
}

export enum ErrorStatus{
 serverError,
 notFoundError,
 badRequestError,
 unAuthorizedError,
 frontError
}
export class ErrorModel implements IErrorModel{
    status: ErrorStatus;
    hasValidationError=false;
    error?: { [key: string]: any; };
    message?: string;
    private _noMessage="Problem has happned at server";
    constructor(errorRes:HttpErrorResponse,uilityService: CommonUtilityService){
     
        if (errorRes.error instanceof ErrorEvent) {
            // Client-side errors
            this.status=ErrorStatus.frontError;
            this.message = `Error: ${errorRes.error.message}`;
          } else {
            // Server-side errors
            if(errorRes.status==400 || errorRes.status==422){
                this.status=ErrorStatus.badRequestError;
                if(errorRes.error)
                {
                    this.hasValidationError=true;
                    this.error=uilityService.convertObjPropsToCamleCseString(errorRes.error?.errors||errorRes.error);
                }
                else{
                    this.message=this._noMessage;
                }
            }
            else if(errorRes.status==404){
                this.status=ErrorStatus.notFoundError;
                if(errorRes.error)
                {
                    this.hasValidationError=true;
                    this.error=uilityService.convertObjPropsToCamleCseString(errorRes.error?.errors||errorRes.error);
                }
                else{
                    this.message=this._noMessage;
                }
            }
            else {
                this.status=ErrorStatus.serverError;
                this.message=this._noMessage;
            }
          }
        
    }
}