import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteGuard implements CanActivate {

  constructor(private router:Router) { }

  isNumber(n) {
    return !isNaN(parseFloat(n)) && !isNaN(n - 0);
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    var planType = route.paramMap.get('planType');
    var type = route.paramMap.get('type');
    var id = route.paramMap.get('id');
    if (planType == "prepaid" || planType == "postpaid") {  
      if(id!=null)
      {
        var val = this.isNumber(id);
        if(val == true)
          return true;
        else
        {
          this.router.navigate(['home']);  
          return false;  
        }
      }
      return true;  
    }
    if(planType == "vip" || planType == "port")
    {
      if(type == "prepaid" || type == "postpaid")
      {
        if(id!=null)
        {
          var val = this.isNumber(id);
          if(val == true)
            return true;
          else
          {
            this.router.navigate(['home']);  
            return false;  
          }
        }
      }
      return true;
    }
    else{
      this.router.navigate(['home']);  
      return false;  
    }   
  }
}
