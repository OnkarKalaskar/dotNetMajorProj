import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FavMovie } from './Model/FavMovie';
import { Movie } from './Model/Movie';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  constructor(private http : HttpClient) { }

  getAllMovies() : Observable<any>{
    return this.http.get('https://localhost:7173/api/Movies');
  }

  postMovie(payload : Movie)
  {
    return this.http.post('https://localhost:7173/api/Movies',payload);
  }

  getMovieById(id : number)  : Observable<any>
  {
    return this.http.get('https://localhost:7173/api/Movies/'+id);
  }

  increaseMovieLike(id : number)
  {
    return this.http.patch('https://localhost:7173/api/Movies/IncreaseLike?MovieId='+id,"");
  }

  decreaseMovieLike(id : number)
  {
    return this.http.patch('https://localhost:7173/api/Movies/DecreaseLike?MovieId='+id,"");
  }

  addFavoriteMovie(payload : FavMovie)
  {
    return this.http.post('https://localhost:7173/api/Movies/AddFavMovie',payload);
  }

  removeFavoriteMovie(payload : any)
  {
    return this.http.delete<FavMovie>('https://localhost:7173/api/Movies/RemoveFromFav',payload);
  }

  getAllMediaCategories() : Observable<any>
  {
    return this.http.get('https://localhost:7173/api/MediaCategories');
  }

  getUserFavoriteMovies(id : number) : Observable<any>
  {
    return this.http.get('https://localhost:7173/api/Movies/GetFavMovies/'+id)
  }

}
