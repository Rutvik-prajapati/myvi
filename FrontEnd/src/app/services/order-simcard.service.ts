import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IOrderSim } from '../models/orderSim';

@Injectable({
  providedIn: 'root'
})
export class OrderSIMCardService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/OrderSIMCard';

  buyNewSimCard(orderDetail:IOrderSim)
  {
    return this.http.post(`${this._baseUrl}/BuyNewSIM`,orderDetail);
  }
}
