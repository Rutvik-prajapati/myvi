import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlan } from 'src/app/models/plan';
import { IPlanType } from 'src/app/models/planType';
import { ISimType } from 'src/app/models/simType';
import { PlanTypeService } from 'src/app/services/plan-type.service';
import { PlanService } from 'src/app/services/plan.service';
import { SimTypeService } from 'src/app/services/sim-type.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  constructor(private plan:PlanService,private plantype:PlanTypeService,private simtype:SimTypeService,private router:Router,private route: ActivatedRoute) { }

  planDetail:IPlan;
  plans:IPlan[];
  planType:string = "unlimited";
  planTypeList:IPlanType[];
  simTypeList:ISimType[];
  defaultPlanTypeName:string;
  type:string;

  ngOnInit(): void {
    this.type = this.capitalize(this.route.snapshot.paramMap.get('type'));
    if(this.type == "Prepaid")
    {
      this.defaultPlanTypeName = "Unlimited";
      this.planType = "unlimited";
    }
    else
    {
      this.defaultPlanTypeName = "Individual";
      this.planType = "individual";
    }
    this.getAllSIMType();
    window.scrollTo(0, 0);
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

  getAllPlanType()
  {
    this.plantype.getAllPlanType()
    .subscribe((res:any)=>{
      console.log(res);
      this.planTypeList = res.filter(x=>x.simTypeId == this.getSimTypeId(this.type));
      this.getPlanList(this.getPlanTypeId(this.defaultPlanTypeName));
      console.log(this.planTypeList);
    })
  }

  getPlanTypeId(name:string):number{
    var val = this.planTypeList.find(x => x.name === name);
    console.log(val);
    return val.id;
  }

  GetPlans(item:IPlanType)
  {
    this.planType = item.name.toLowerCase();
    console.log(this.planType);
    this.getPlanList(item.id);
  }

  getPlanList(id:number){
    this.plan.getPlanListByPlanTypeId(id)
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
      this.getAllPlanType();
      console.log(this.planTypeList);
    })
  }

  getSimTypeId(name:string):number{
    var val = this.simTypeList.find(x => x.name === name);
    console.log(val);
    return val.id;
  }

  capitalize(s:string) {
    return s.charAt(0).toUpperCase() + s.slice(1).toLowerCase();
  }

}
