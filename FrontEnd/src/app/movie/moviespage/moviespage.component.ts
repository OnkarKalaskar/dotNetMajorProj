import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/role/user.service';
import { MediaCategory } from '../Model/MediaCategory';
import { Movie } from '../Model/Movie';
import { MoviesService } from '../movies.service';

@Component({
  selector: 'app-moviespage',
  templateUrl: './moviespage.component.html',
  styleUrls: ['./moviespage.component.css']
})
export class MoviespageComponent implements OnInit {
  AllMovies: Movie[] = [];
  isLogin: boolean = false;
  allCategories: MediaCategory[] = [];
  movieCategoryId: number[] = [];
  //currentPageNumber : any;

  Action : Movie[] = []
  Drama : Movie[] = []
  Horror : Movie[] = []
  Comedy : Movie[] = []
  Adventure : Movie[] = []
  Animation : Movie[] = []
  ScienceFiction : Movie[] = []


  constructor(private moviesService: MoviesService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getMovies();
    this.getMediaCategories();
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

  getMovies() {
    //add movies to respective array
    this.moviesService.getAllMovies().subscribe({
      next: (res : Movie[]) => {
        this.AllMovies = res;
        console.log(this.AllMovies);

        res.forEach(movie =>{
          if(movie.categoryId ==1)
          {
            this.Action.push(movie);
          }
          if(movie.categoryId ==2)
          {
            this.Drama.push(movie);
          }
          if(movie.categoryId ==3  || movie.categoryId ==5)
          {
            this.Horror.push(movie);
          }
          if(movie.categoryId ==4  || movie.categoryId ==5)
          {
            this.Comedy.push(movie);
          }
          if(movie.categoryId ==5  || movie.categoryId ==3)
          {
            this.Adventure.push(movie);
          }
          if(movie.categoryId ==6  || movie.categoryId ==2)
          {
            this.Animation.push(movie);
          }
          if(movie.categoryId ==7 || movie.categoryId ==4)
          {
            this.ScienceFiction.push(movie);
          }

        })
        
      }
    })
  }

  getMediaCategories() {
    this.moviesService.getAllMediaCategories().subscribe({
      next: (res) => {
        res.forEach((element: any) => {
          if (element.categoryId < 8) {
            this.movieCategoryId.push(element.categoryId);
            this.allCategories.push(element);
          }
        });
        //this.allCategories = res;

      }
    })
  }
}
