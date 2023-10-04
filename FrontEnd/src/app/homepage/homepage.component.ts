import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from '../movie/Model/Movie';
import { MoviesService } from '../movie/movies.service';
import { Song } from '../song/model/song';
import { SongService } from '../song/song.service';
import { TvShow } from '../tvshow/model/tvshow';
import { TvshowsService } from '../tvshow/tvshows.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  
  allMovies : Movie[] =[]
  topTrendingMovies : Movie[] = []
  topTrendingSongs : Song[] = []
  topTrendingTvShows : TvShow[]=[]


  constructor(private router : Router,private movieService : MoviesService, private songService : SongService, private tvShowService : TvshowsService){}

  ngOnInit(): void {
    this.getAllData();
  }

  getAllData()
  {
    this.movieService.getAllMovies().subscribe({
      next : (res : Movie[]) => {
        res.sort((a,b) => b.likes-a.likes);
        this.topTrendingMovies = res.slice(0,12);
      }
    })
    
    this.songService.getAllSongs().subscribe({
      next : (res : Song[]) => {
        res.sort((a,b) => a.likes-b.likes);
        this.topTrendingSongs = res.slice(0,12);
      }
    })

    this.tvShowService.getAllTvShows().subscribe({
      next : (res : TvShow[])=> {
        res.sort((a,b) => a.likes-b.likes);
        this.topTrendingTvShows = res.slice(0,12);
      }
    })
  }

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
