import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Song } from '../model/song';

@Component({
  selector: 'app-songcontainer',
  templateUrl: './songcontainer.component.html',
  styleUrls: ['./songcontainer.component.css']
})
export class SongcontainerComponent {
  @Input() songsOfCategory : Song[] = []
  currentPageNumber : any;

  constructor(private router : Router){}

  checkUserLogin(songId: number) {
    let token = sessionStorage.getItem("access_token");

    if (token != null) {
      this.router.navigate(['songs/songdetails/' + songId]);
    }
    else {

      this.router.navigate(['login']);
    }
  }
}
