import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRecharge } from '../models/recharge';

@Injectable({
  providedIn: 'root'
})
export class RechargeService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/Recharge';

  // NewRecharge(id:number,Amount:number):Observable<any>{
  //   let params = {planId:id,amount:Amount};
  //   return this.http.post(`${this._baseUrl}/NewRecharge`,'', {
  //     headers: new HttpHeaders({
  //       'Content-Type': 'application/json'
  //     }),
  //     params: params,
  //     responseType: "json"
  //   });
  // }

  NewRecharge(rechargeData:IRecharge):Observable<any>{
    return this.http.post(`${this._baseUrl}/NewRecharge`,rechargeData);
  }

  // checkout(_orderId:string, _paymentId:string, _signature:string):Observable<any>{
  //   let params = {orderId:_orderId, paymentId:_paymentId, signature:_signature};
  //   return this.http.post(`${this._baseUrl}/checkout`,'', {
  //     headers: new HttpHeaders({
  //       'Content-Type': 'application/json'
  //     }),
  //     params: params,
  //     responseType: "json"
  //   });
  // }

  checkout(rechargeData:IRecharge):Observable<any>{
    return this.http.post(`${this._baseUrl}/checkout`,rechargeData);
  }

}
