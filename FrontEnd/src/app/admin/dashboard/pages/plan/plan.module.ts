import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlanRoutingModule } from './plan-routing.module';
import { PlanComponent } from './plan.component';
import { AddPlanComponent } from './add-plan/add-plan.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    PlanComponent,
    AddPlanComponent
  ],
  imports: [
    CommonModule,
    PlanRoutingModule,
    ReactiveFormsModule
  ]
})
export class PlanModule { }
