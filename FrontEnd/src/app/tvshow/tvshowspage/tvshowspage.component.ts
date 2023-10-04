import { Component, OnInit } from '@angular/core';
import { TvShow } from '../model/tvshow';
import { TvshowsService } from '../tvshows.service';
import { MediaCategory } from '../model/MediaCategory';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tvshowspage',
  templateUrl: './tvshowspage.component.html',
  styleUrls: ['./tvshowspage.component.css']
})
export class TvshowspageComponent implements OnInit {

  TvShows: TvShow[] = [];
  isLogin: boolean = false;
  allCategories: MediaCategory[] = [];
  movieCategoryId: number[] = [];


  
  Action : TvShow[] = []
  Drama : TvShow[] = []
  Horror : TvShow[] = []
  Comedy : TvShow[] = []
  Adventure : TvShow[] = []
  Animation : TvShow[] = []
  ScienceFiction : TvShow[] = []

  constructor(private tvshowService: TvshowsService , private router :Router) { }

  ngOnInit() {
    this.getTvShows();
    this.getMediaCategories();
    
  }


  getTvShows() {
    this.tvshowService.getAllTvShows().subscribe({
      next: (res) => {
        console.log(res);

        res.forEach((tvShow : TvShow) =>{
          if(tvShow.categoryId ==1 || tvShow.categoryId ==7)
          {
            this.Action.push(tvShow);
          }
          if(tvShow.categoryId ==2|| tvShow.categoryId ==6)
          {
            this.Drama.push(tvShow);
          }
          if(tvShow.categoryId ==3 || tvShow.categoryId ==5)
          {
            this.Horror.push(tvShow);
          }
          if(tvShow.categoryId ==4 || tvShow.categoryId ==7)
          {
            this.Comedy.push(tvShow);
          }
          if(tvShow.categoryId ==5 || tvShow.categoryId ==7)
          {
            this.Adventure.push(tvShow);
          }
          if(tvShow.categoryId ==6 || tvShow.categoryId ==7)
          {
            this.Animation.push(tvShow);
          }
          if(tvShow.categoryId ==7|| tvShow.categoryId ==1)
          {
            this.ScienceFiction.push(tvShow);
          }
      })
    }
    })
  }

  getMediaCategories() {
    this.tvshowService.getAllMediaCategories().subscribe({
      next: (res) => {
        res.forEach((element: any) => {
          if (element.categoryId < 8) {
            this.movieCategoryId.push(element.categoryId);
            this.allCategories.push(element);
          }
        })
      }
    })
  }

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
