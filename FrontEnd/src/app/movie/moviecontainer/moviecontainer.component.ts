import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from '../Model/Movie';

@Component({
  selector: 'app-moviecontainer',
  templateUrl: './moviecontainer.component.html',
  styleUrls: ['./moviecontainer.component.css']
})
export class MoviecontainerComponent {

  @Input() moviesOfCategory : Movie[] = []
  currentPageNumber : any = 1;

  constructor(private router : Router){}

  checkUserLogin(movieId: number) {
    let token = sessionStorage.getItem("access_token");

    if (token != null) {
      this.router.navigate(['movies/moviedetails/' + movieId]);
    }
    else {
      this.router.navigate(['login']);
    }
  }

}
