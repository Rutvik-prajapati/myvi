import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IRegister } from '../models/register';

@Injectable({
  providedIn: 'root'
})
export class UserRegisterService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl;

  registerUser(registerData:IRegister)
  {
    return this.http.post(`${this._baseUrl}/authenticate/register`,registerData);
  }
}
