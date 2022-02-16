import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IPlan } from 'src/app/models/plan';
import { PlanService } from 'src/app/services/plan.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {
  planDetail:IPlan;
  plans:IPlan[];
  planTypeId:number = 1;

  constructor(private route:Router,private plan:PlanService) { }

  ngOnInit(): void {
    this.GetPlans();
  }

  GetPlans()
  {
    this.plan.getPlanListByPlanTypeId(this.planTypeId)
    .subscribe((res:any)=>{
      console.log(res);
      this.plans = res;
      console.log(this.plans);
    })
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
}
