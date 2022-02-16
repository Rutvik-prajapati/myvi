import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/plan';

  getAllPlans():Observable<any>{
    return this.http.get(`${this._baseUrl}/GetAllPlan`);
  }

  getPlanListByPlanTypeId(id:number){
    let params = {planTypeid:id};
    return this.http.post(`${this._baseUrl}/GetPlanListByPlanTypeId`,'', {
          headers: new HttpHeaders({
            'Content-Type': 'application/json'
          }),
          params: params,
          responseType: "json"
    });
  }

  getPlanListBySIMTypeId(id:number){
    let params = {simTypeId:id};
    return this.http.post(`${this._baseUrl}/GetPlanListBySIMTypeId`,'', {
          headers: new HttpHeaders({
            'Content-Type': 'application/json'
          }),
          params: params,
          responseType: "json"
    });
  }

  getPlanDetailById(planId):Observable<any>{
    let params = {id:planId};
    return this.http.get(`${this._baseUrl}/GetPlanById`,{params});
  }

}
