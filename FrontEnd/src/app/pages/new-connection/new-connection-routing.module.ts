import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NewConnectionComponent } from './new-connection.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { AuthGuard } from 'src/app/services/auth-guard.service';
import { RouteGuard } from 'src/app/services/route-guard.service';

const routes: Routes = [
  { 
    path: '', 
    component: NewConnectionComponent
  },
  { 
    path: ':id/user-detail', 
    component: UserDetailComponent,
    canActivate:[AuthGuard,RouteGuard]
  },
  { 
    path: ':type/:id/user-detail', 
    component: UserDetailComponent,
    canActivate:[AuthGuard,RouteGuard]
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
export class NewConnectionRoutingModule { }
