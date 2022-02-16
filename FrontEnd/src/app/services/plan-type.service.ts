import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanTypeService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/PlanType';

  getAllPlanType():Observable<any>{
    return this.http.get(`${this._baseUrl}/GetAllPlanType`);
  }
}
