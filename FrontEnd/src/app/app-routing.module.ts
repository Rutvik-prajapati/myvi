import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { AuthGuard } from './services/auth-guard.service';
import { RoleGuard } from './services/role-guard.service';

const routes: Routes = [
  { 
    path:'', 
    redirectTo : '/home', 
    pathMatch:'full'
  },
  { 
    path: 'home',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule),
    data: {title: 'MyVi login'}
  },
  {
    path: 'register',
    loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule),
    data: {title: 'MyVi register'}
  },
  {
    path: 'admin',
    canActivate: [RoleGuard,AuthGuard],
    loadChildren: () => import('./admin/dashboard/dashboard.module').then(m => m.DashboardModule),
    data: {title: 'MyVi admin'}
  },
  {
    path:'**',
    component:PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
