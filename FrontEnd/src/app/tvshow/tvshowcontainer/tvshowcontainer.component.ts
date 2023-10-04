import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { TvShow } from '../model/tvshow';

@Component({
  selector: 'app-tvshowcontainer',
  templateUrl: './tvshowcontainer.component.html',
  styleUrls: ['./tvshowcontainer.component.css']
})
export class TvshowcontainerComponent {
  
  @Input() tvShowsOfCategory : TvShow[] = []
  currentPageNumber : any;

  constructor(private router : Router){}

  checkUserLogin(tvShowId: number) {
    let token = sessionStorage.getItem("access_token");

    if (token != null) {
      this.router.navigate(['tvshows/tvshowdetails/' + tvShowId]);
    }
    else {

      this.router.navigate(['login']);
    }
  }
}
