import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/Users';

  getUserDetailById(userId):Observable<any>{
    let params = {id:userId};
    return this.http.get(`${this._baseUrl}/GetUserById`,{params});
  }
}
