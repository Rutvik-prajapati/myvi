import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PlanComponent } from './plan.component';
import { AddPlanComponent } from './add-plan/add-plan.component';

const routes: Routes = [
  { 
    path: '', 
    component: PlanComponent
  },
  { 
    path: 'add-plan', 
    component: AddPlanComponent
  },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class PlanRoutingModule { }
