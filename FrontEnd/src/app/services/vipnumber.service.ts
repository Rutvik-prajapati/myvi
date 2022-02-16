import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrderSim } from '../models/orderSim';

@Injectable({
  providedIn: 'root'
})
export class VipnumberService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/VIPNumber';

  getAllVIPNumber():Observable<any>{
    return this.http.get(`${this._baseUrl}/GetAllvipNumber`);
  }

  buyNewVIPSimCard(orderDetail:IOrderSim)
  {
    return this.http.post(`${this._baseUrl}/OrderVIPNumber`,orderDetail);
  }
}
