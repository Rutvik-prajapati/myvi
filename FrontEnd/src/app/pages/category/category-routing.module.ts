import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmMobileNoComponent } from 'src/app/shared/component/confirm-mobile-no/confirm-mobile-no.component';
import { AuthGuard } from 'src/app/services/auth-guard.service';

const routes: Routes = [
  { 
    path: '', 
    component: CategoryComponent
  },
  { 
    path: 'mobileNo-conform/:id',
    component: ConfirmMobileNoComponent,
    canActivate:[AuthGuard]
  }
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
export class CategoryRoutingModule { }
