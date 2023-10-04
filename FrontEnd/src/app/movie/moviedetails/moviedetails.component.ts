import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UserService } from 'src/app/role/user.service';
import { Movie } from '../Model/Movie';
import { FavMovie } from '../Model/FavMovie';
import { MoviesService } from '../movies.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-moviedetails',
  templateUrl: './moviedetails.component.html',
  styleUrls: ['./moviedetails.component.css']
})
export class MoviedetailsComponent implements OnInit{

  currentMovie : any;
  allMoviesData :  Movie[] = [];
  movieId : number = 0;
  userId = sessionStorage.getItem("userId");
  moviesOfSameGenre : Movie[] = [];
  currentMovieCategory : number = 0;
  //to toggle favorite button
  isFavorite : boolean | undefined;
  showToaster : boolean = false;
  currentPageNumber : any;


  constructor(private route : ActivatedRoute, private movieService : MoviesService, private router : Router, private userService : UserService, private toastr : ToastrService){}

  ngOnInit() { 
    console.log("ng on init called");
    this.getCurrentMovieId();
    this.getCurrentMovie();  
  }
  
  getCurrentMovieId()
  {
    this.route.params.subscribe((params: Params) =>{
      this.movieId = params['id'];
      console.log(this.movieId);
    })
  }

  //get current movie data
  getCurrentMovie()
  {
      this.movieService.getMovieById(this.movieId).subscribe({
        next : (res) => {
          this.currentMovie = res;
          this.currentMovieCategory = this.currentMovie.categoryId;
          
          this.movieService.getAllMovies().subscribe({
            next : (res : Movie[])=>{
              res.forEach((movie : Movie) =>{
                  if(movie.categoryId == this.currentMovieCategory)
                  {
                    this.moviesOfSameGenre.push(movie);
                  } 
              });            
          }
        })

        

        
        },
        error : (err)=>{
          this.router.navigate(["unauthorized"]); 
          console.log(err);
        }
      })
  }

  //increse like
  increaseLike()
  {
    this.movieService.increaseMovieLike(this.currentMovie.movieId).subscribe({
      next : (res) => {
      }
    });
  }

  //decrease like
  decreaseLike()
  {
    
    this.movieService.decreaseMovieLike(this.currentMovie.movieId).subscribe({
      next : (res) => {
      }
    }); 

  }

  playMovie(id : number)
  {
    this.moviesOfSameGenre = [];
    this.router.navigate(["movies/moviedetails/"+id]);
    this.getCurrentMovieId();
    this.getCurrentMovie(); 
  }

  refresh()
  {
    window.location.reload();
  }

  addMovieToUserFavorites(movieId : number)
  {
    let userId : any = sessionStorage.getItem("userId");
    
    let favMovieObj : FavMovie = {
      userId : userId,
      movieId : movieId
    };

    this.movieService.addFavoriteMovie(favMovieObj).subscribe({
      next: (res : any) => {
        if(res.message == 'Already in user favorites')
        {
          this.toastr.success("Already in user favorites");
        }
        else if(res.message == "Added to favorites"){
          this.toastr.success("Added to favorites");
        }
      },

      error : (err)=>{
        this.router.navigate(["unauthorized"]); 
        console.log(err);
      }
    });
  }

  removeMovieToUserFavorites(movieId : number)
  {
    let userId : any = sessionStorage.getItem("userId");

    let favMovieObj : FavMovie = {
      userId : userId,
      movieId : movieId
    };

    this.movieService.removeFavoriteMovie(favMovieObj).subscribe({
      next : (res : any) =>{
        if(res.message == "Removed From Favorites")
        {
          this.toastr.success("Removed from user favorites");
        }
      }
    })

  }



}
