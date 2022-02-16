import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ILogin } from '../models/login';
import { IRegister } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http : HttpClient,private route : Router) { }
  private tokenTimer: any;

  private _baseUrl = environment.baseUrl + '/authenticate';

  stringTime:string = localStorage.getItem('expireTime');
  expireTime = new Date(this.stringTime);
  now = new Date();

  loginUser(loginData:ILogin)
  {
    return this.http.post(`${this._baseUrl}/login`,loginData).pipe(
      map((res:any) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role);
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('contactNo', res.mobileNo);
        localStorage.setItem('expireTime',new Date(res.expiration).toString());

        this.setAuthTimer();

        return res;
      })
    );
  }

  setAuthTimer(){
    let expiration = new Date(localStorage.getItem('expireTime')!);
    if (expiration.getTime() > Date.now()) {
      let duration = expiration.getTime() - Date.now();
      console.log(duration);
      console.log('setting timer:' + duration);

      this.tokenTimer = setTimeout(() => {
        this.logout();
      }, duration);
    }
    else {
      localStorage.clear();
    }
  }

  getToken()
  {
    return localStorage.getItem('token');
  }

  registerUser(registerData:IRegister)
  {
    return this.http.post(`${this._baseUrl}/register`,registerData);
  }

  checkLogged():boolean
  {
    if(localStorage.getItem('token') == null)
      return false;
    else
      return true;
  }

  logout()
  {
    localStorage.clear();
    this.route.navigate['home'];
  }
}
