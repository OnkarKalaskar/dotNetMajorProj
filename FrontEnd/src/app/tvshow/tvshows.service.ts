import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FavTvShow } from './model/favtvshow';

@Injectable({
  providedIn: 'root'
})
export class TvshowsService {

  constructor(private http : HttpClient) { }

  getAllTvShows() : Observable<any>
  {
      return this.http.get('https://localhost:7173/getAllTvShows');
  }
  getAllMediaCategories() : Observable<any>
  {
    return this.http.get('https://localhost:7173/api/MediaCategories');
  }

  getTvShowById(id:number) : Observable<any>
  {
    return this.http.get('https://localhost:7173/getTvShow/'+id);
  }

  addToFav(payload : FavTvShow) :Observable<any>
  {
    return this.http.post('https://localhost:7173/addtoFavTvShow',payload);
  }

  increaseLike(id:number) : Observable<any>
  {
    return this.http.patch('https://localhost:7173/increaseLike/'+id,"")
  }

  decreaseLike(id:number) : Observable<any>
  {
    return this.http.patch('https://localhost:7173/decreaseLike/'+id,"")
  }
}
