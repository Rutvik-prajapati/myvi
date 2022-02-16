import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { OrderComponent } from './component/order/order.component';
import { TempComponent } from './component/temp/temp.component';
import { RoleGuard } from 'src/app/services/role-guard.service';
import { AuthGuard } from 'src/app/services/auth-guard.service';

const routes: Routes = [
  { 
    path: '', 
    component: DashboardComponent,
    children : [
      { 
        path: 'order', 
        component:OrderComponent
      },
      { 
        path: 'temp', 
        component:TempComponent
      },
      { 
        path: 'plan', 
        loadChildren: () => import('./pages/plan/plan.module').then(m => m.PlanModule),
        canActivate: [RoleGuard,AuthGuard],
      },
    ] 
  },
  // { 
  //   path: 'plans', 
  //   loadChildren: () => import('../dashboard/pages/plans/plans.module').then(m => m.PlansModule)
  // },
  // { 
  //   path: 'order', 
  //   loadChildren: () => import('../dashboard/pages/order/order.module').then(m => m.OrderModule)
  // }
  // { 
  //   path: 'plans', 
  //   component:PlansComponent
  // },
  
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
export class DashboardRoutingModule { }
