import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { TvShow } from 'src/app/tvshow/model/tvshow';
import { Movie } from 'src/app/movie/Model/Movie';
import { Song } from 'src/app/song/model/song';
import { Router } from '@angular/router';
import { FavMovie } from 'src/app/movie/Model/FavMovie';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-userfavourite',
  templateUrl: './userfavourite.component.html',
  styleUrls: ['./userfavourite.component.css']
})
export class UserfavouriteComponent implements OnInit {

  userId: any
  AllFavTvShow: TvShow[] = []
  AllFavMovie: Movie[] = []
  AllFavSong : Song[] = []
  Type: string[] = ["Movie", "TvShow", "Song"];
  currentPageNumber : any;

  constructor(private userService: UserService,private router:Router, private toastr : ToastrService) {
    this.userId = sessionStorage.getItem("userId");
  }

  ngOnInit(): void {
    this.getUserFavTvShows()
    this.getUserFavMovies()
    this.getUserFavSongs()
  }

  getUserFavTvShows() {
    //let userId : any = sessionStorage.getItem("userId");
    this.userService.getUserFavouritesTvShows(this.userId).subscribe({
      next: (res) => {
        this.AllFavTvShow = res
      }
    })
  }

  getUserFavMovies() {
    this.userService.getUserFavMovies(this.userId).subscribe({
      next: (res) => {
        this.AllFavMovie = res

      }
    })
  }

  getUserFavSongs() {
    this.userService.getUserFavSongs(this.userId).subscribe({
      next: (res) => {
        this.AllFavSong = res
        
      }
    })
  }

  removeFavMovie(movieId : number)
  {
    let userId : any = sessionStorage.getItem("userId");

    this.userService.removeMovieFromFavorites(movieId,userId).subscribe({
      next : (res: any) =>{
        this.toastr.success("removed from favorites");
        window.location.reload();
      }
    });
  }

  removeFavTvShow(tvShowId : number)
  {
    let userId : any = sessionStorage.getItem("userId");

    this.userService.removeTvShowFromFavorites(tvShowId,userId).subscribe({
      next : (res : any) =>{
        this.toastr.success("removed from favorites");
        window.location.reload();
      }
    })
  }

  removeFavSong(songId : number)
  {
    let userId : any = sessionStorage.getItem("userId");

    this.userService.removeSongFromFavorites(songId,userId).subscribe({
      next : (res : any) =>{
        this.toastr.success("removed from favorites");
        window.location.reload();
      }
    })

  }

  routeToMovie(movieId:number)
  {
    this.router.navigate(['movies/moviedetails/' + movieId]);
  }

  routeToTvShow(tvShowId:number){
    this.router.navigate(['tvshows/tvshowdetails/' + tvShowId]);
  }

  routeToSong(songId:number){
    this.router.navigate(['songs/songdetails/' + songId]);
  }

}
