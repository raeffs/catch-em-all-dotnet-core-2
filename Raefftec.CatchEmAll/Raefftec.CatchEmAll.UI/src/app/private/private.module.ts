import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrivateRoutingModule } from './private-routing.module';
import { PrivateComponent } from './private.component';

@NgModule({
  imports: [
    CommonModule,
    PrivateRoutingModule
  ],
  declarations: [PrivateComponent]
})
export class PrivateModule { }
