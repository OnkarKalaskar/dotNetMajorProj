import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientdashboardRoutingModule } from './clientdashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatModule } from 'src/mat.module';
import { DialogComponent } from './dialog/dialog.component';
import { EditdialogComponent } from './editdialog/editdialog.component';
import { TvshowdialogComponent } from './tvshowdialog/tvshowdialog.component';
import { TvshoweditdialogComponent } from './tvshoweditdialog/tvshoweditdialog.component';
import { SongdialogComponent } from './songdialog/songdialog.component';
import { SongeditdialogComponent } from './songeditdialog/songeditdialog.component';


@NgModule({
  declarations: [
    DashboardComponent,
    DialogComponent,
    EditdialogComponent,
    TvshowdialogComponent,
    TvshoweditdialogComponent,
    SongdialogComponent,
    SongeditdialogComponent
  ],
  imports: [
    CommonModule,
    ClientdashboardRoutingModule,
    MatModule
  ]
})
export class ClientdashboardModule { }
