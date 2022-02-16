import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrderSim } from '../models/orderSim';

@Injectable({
  providedIn: 'root'
})
export class PortnumberService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/PortNumber';


  buyNewPortSimCard(orderDetail:IOrderSim)
  {
    return this.http.post(`${this._baseUrl}/OrderPortNumber`,orderDetail);
  }

  checkPortNumberExist(_number:string){
    let params = {number:_number};
    return this.http.post(`${this._baseUrl}/CheckPortNumberExist`,'', {
          headers: new HttpHeaders({
            'Content-Type': 'application/json'
          }),
          params: params,
          responseType: "json"
    });
  }
}
