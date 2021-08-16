import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from "../../../environments/environment";
import { User} from '../../models/User';
import {map} from 'rxjs/operators'
import { Router } from '@angular/router';

@Injectable()  
export class AuthService {
  private currentUserName="currentUser";
  private tokenName="token";
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient,private router:Router) {
    const _user=JSON.parse(localStorage.getItem(this.currentUserName));
      this.currentUserSubject = new BehaviorSubject<User>(_user);
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
      return this.currentUserSubject.value;
  }

  login(loginData:any) {
      return this.http.post<any>(`${environment.apiUrl}/auth/SignIn`, loginData)
          .pipe(map(data => {
              // login successful if there's a jwt token in the response
              if (data && data.token) {
                this.storeUserDataAndCredentials({...data,username:loginData['username']});
              }

              return data;
          }));
  }
  
  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem(this.currentUserName);
      localStorage.removeItem(this.tokenName);
      this.currentUserSubject.next(null);
      this.router.navigate(['/']).then(()=>location.reload());
  }
  
  isAuthenticated():boolean{
   return !!this.token;
  }
  updateUserData(data){
    this.storeUserDataAndCredentials(data);
  }
  private setUserData(user:User){
    localStorage.setItem(this.currentUserName, JSON.stringify(user));
    this.currentUserSubject.next(user);
  }
  public get token(){
    return localStorage.getItem(this.tokenName);
  }
  private storeUserDataAndCredentials(data){
    this.setUserData({userName:data['username']} as any);
    localStorage.setItem(this.tokenName,data.token);
  }
}
