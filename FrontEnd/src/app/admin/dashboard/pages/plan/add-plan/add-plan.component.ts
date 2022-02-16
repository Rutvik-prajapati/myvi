import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IPlanType } from 'src/app/models/planType';
import { PlanTypeService } from 'src/app/services/plan-type.service';

@Component({
  selector: 'app-add-plan',
  templateUrl: './add-plan.component.html',
  styleUrls: ['./add-plan.component.css']
})
export class AddPlanComponent implements OnInit {

  constructor(private planType:PlanTypeService) { }
  planForm:FormGroup;
  planTypeList:IPlanType[];

  ngOnInit(): void {
    this.planForm = new FormGroup({
      "planTypeId":new FormControl('',[Validators.required]),
      "price":new FormControl('',[Validators.required]),
      "call":new FormControl(''),
      "talktime":new FormControl(''),
      "data":new FormControl(''),  
      "sms":new FormControl(''),
      "connection":new FormControl('',[Validators.required,Validators.minLength(3),Validators.pattern('[a-zA-Z]*')]),
      "validity":new FormControl('',[Validators.required,Validators.minLength(6),Validators.maxLength(6),Validators.pattern('[0-9]*')]),
      "benefits":new FormControl('',[Validators.required,Validators.minLength(6),Validators.maxLength(6),Validators.pattern('[0-9]*')])                                                                                                                                           
    });
    this.getPlanType();
  }

  getPlanType(){
    this.planType.getAllPlanType()
    .subscribe((res:any)=>{
      console.log(res);
      this.planTypeList = res;
      console.log(this.planTypeList);
    })
  }

}
