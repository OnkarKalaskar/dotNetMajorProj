import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { UserService } from '../user.service';
import { TvShow } from 'src/app/tvshow/model/tvshow';
import { Movie } from 'src/app/movie/Model/Movie';
import { Song } from 'src/app/song/model/song';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
  selector: 'app-usersearch',
  templateUrl: './usersearch.component.html',
  styleUrls: ['./usersearch.component.css']
})

export class UsersearchComponent implements OnInit, OnChanges {
 
  AllSearchedTvShow: TvShow[] = []
  AllSearchedMovie: Movie[] = []
  AllSearchedSong : Song[] = []
  Type: string[] = ["Movie", "TvShow", "Song"];
  @Input()
  searchKey:string='';
  currentPageNumber : any;

  constructor(private userService: UserService,private router:Router,private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.setSearchKey();
    this.getUserSearchedTvShows();
    this.getUserSearchedMovies();
    this.getUserSearchedSongs();
  }

  ngOnChanges(){
    console.log("on changes called");
  }

  setSearchKey()
  {
    this.route.params.subscribe((params: Params) =>{
      this.searchKey = params['searchkey'];
      console.log(this.searchKey);
    })
  }

  getUserSearchedMovies() {
    this.userService.getUserSearchedMovies(this.searchKey).subscribe({
      next: (res) => {
        this.AllSearchedMovie = res;      
      }
    })
  }

  getUserSearchedTvShows() {
    this.userService.getUserSearchedTvShows(this.searchKey).subscribe({
      next: (res) => {
        this.AllSearchedTvShow = res;        
      }
    })
  }

  getUserSearchedSongs() {
    this.userService.getUserSearchedSongs(this.searchKey).subscribe({
      next: (res) => {
        this.AllSearchedSong = res;        
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