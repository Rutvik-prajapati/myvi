import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SimTypeService {

  constructor(private http : HttpClient) { }

  private _baseUrl = environment.baseUrl + '/SIMType';

  getAllSimType():Observable<any>{
    return this.http.get(`${this._baseUrl}/GetAllSimType`);
  }
  
}
