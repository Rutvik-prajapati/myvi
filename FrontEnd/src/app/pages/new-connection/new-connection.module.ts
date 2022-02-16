import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewConnectionComponent } from './new-connection.component';
import { NewConnectionRoutingModule } from './new-connection-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    NewConnectionComponent,
    UserDetailComponent
  ],
  imports: [
    CommonModule,
    NewConnectionRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class NewConnectionModule { }
