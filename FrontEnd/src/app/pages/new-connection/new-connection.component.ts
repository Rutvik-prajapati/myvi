import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlan } from 'src/app/models/plan';
import { ISimType } from 'src/app/models/simType';
import { PlanService } from 'src/app/services/plan.service';
import { SimTypeService } from 'src/app/services/sim-type.service';
declare var $: any;

@Component({
  selector: 'app-new-connection',
  templateUrl: './new-connection.component.html',
  styleUrls: ['./new-connection.component.css']
})
export class NewConnectionComponent implements OnInit {
 
  constructor(private route: ActivatedRoute,private plan:PlanService,private _route:Router,private simtype:SimTypeService) { }
  planType:string;
  plans:IPlan[];
  planDetail:IPlan;
  temp:string;
  simTypeList:ISimType[];

  ngOnInit(): void {
    this.planType = this.route.snapshot.paramMap.get('planType');
    this.getAllSIMType();
    this.onload();
    window.scrollTo(0, 0)
  }

  onload(){
    setTimeout(() => {
      if(this.planType=="prepaid" || this.planType=="postpaid")
      {
        this.getPlansByName(this.planType);
      }
      if(this.planType == "vip" || this.planType == "port")
      {
        this.getPlansByName("prepaid");
        this.temp = "prepaid";
      }
    }, 200);
    
  }

  onClick(type:string)
  {
    if(this.planType == "vip" || this.planType == "port")
    {
       if(this.temp != "prepaid")
       {
        this.getPlansByName("prepaid")
        this.temp="prepaid";
       }
       else
       {
        this.getPlansByName("postpaid")
        this.temp="postpaid";
       }
    }
    else
    {
       this.navigate(type);
    } 
  }

  getPlansByName(type:string){
    var val = this.simTypeList.find(x => x.name === this.capitalize(type));
    this.GetPlans(val.id);
  }

  navigate(type:string)
  {    
    this._route.navigate(['home/new-connection',type]);
    setTimeout(() => {
      window.location.reload();
    }, 300);
  }
 

  planDetails(planId:number)
  {
    this.plan.getPlanDetailById(planId)
    .subscribe((res:any)=>{
      console.log(res);
      this.planDetail = res;
      console.log(this.planDetail);
    })
  }

  GetPlans(id:number)
  {
    this.plan.getPlanListBySIMTypeId(id)
    .subscribe((res:any)=>{
      console.log(res);
      this.plans = res;
      console.log(this.plans);
    })
  }

  getAllSIMType()
  {
    this.simtype.getAllSimType()
    .subscribe((res:any)=>{
      console.log(res);
      this.simTypeList = res;
      console.log(this.simTypeList);
    })
  }

  capitalize(s:string) {
    return s.charAt(0).toUpperCase() + s.slice(1).toLowerCase();
  }

  ngAfterViewInit(){
    $(document).ready(function(){
     console.log("hello");
    });
  }
}
