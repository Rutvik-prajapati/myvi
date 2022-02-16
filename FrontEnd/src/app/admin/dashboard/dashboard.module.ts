import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { OrderComponent } from './component/order/order.component';
import { TempComponent } from './component/temp/temp.component';

@NgModule({
  declarations: [
    DashboardComponent,
    OrderComponent,
    TempComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
