import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { AdvertComponent } from './advert/advert.component';
import { RechargeComponent } from './recharge/recharge.component';
import { BannerComponent } from './banner/banner.component';
import { QuickLinksComponent } from './quick-links/quick-links.component';
import { SimDeliverComponent } from './sim-deliver/sim-deliver.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PlansComponent } from './plans/plans.component';
import { SplitPipe } from 'src/app/pipe/split.pipe';
import { ReactiveFormsModule } from '@angular/forms';
import { CategoryModule } from '../category/category.module';



@NgModule({
  declarations: [
    HomeComponent,
    AdvertComponent,
    RechargeComponent,
    BannerComponent,
    QuickLinksComponent,
    SimDeliverComponent,
    PlansComponent,
    SplitPipe
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule,
    CategoryModule,
    ReactiveFormsModule
  ],
  exports:[
    PlansComponent
  ]
})
export class HomeModule { }
