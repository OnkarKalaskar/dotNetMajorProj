import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatModule } from 'src/mat.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomepageComponent } from './homepage/homepage.component';
import { RoleModule } from './role/role.module';
import { MovieModule } from './movie/movie.module';
import { TvshowModule } from './tvshow/tvshow.module';
import { SongModule } from './song/song.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RequestInterceptor } from './request.interceptor';
import { FormsModule } from '@angular/forms';

import { ToastrModule } from 'ngx-toastr';
import { ClientdashboardModule } from './clientdashboard/clientdashboard.module';
import { AuthGuard } from './auth.guard';


@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatModule,
    RoleModule,
    MovieModule,
    TvshowModule,
    SongModule,
    FormsModule,
    ClientdashboardModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor,
    multi: true
  }, AuthGuard ],
  bootstrap: [AppComponent]
})
export class AppModule { }
