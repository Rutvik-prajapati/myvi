import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { RouteGuard } from 'src/app/services/route-guard.service';
import { AuthGuard } from 'src/app/services/auth-guard.service';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent 
  },
  { 
    path: ':type', 
    loadChildren: () => import('../category/category.module').then(m => m.CategoryModule)
  },
  // { 
  //   path: 'postpaid', 
  //   loadChildren: () => import('../postpaid/postpaid.module').then(m => m.PostpaidModule)
  // },
  { 
    path: 'new-connection/:planType', 
    loadChildren: () => import('../new-connection/new-connection.module').then(m => m.NewConnectionModule),
    canActivate:[RouteGuard]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class HomeRoutingModule { }
