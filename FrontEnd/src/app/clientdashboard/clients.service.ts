import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movie } from 'src/app/movie/Model/Movie';
import { MediaCategory } from '../movie/Model/MediaCategory';
import { TvShow } from '../tvshow/model/tvshow';
import { Song } from '../song/model/song';
import { Singer } from '../singer/model/singer';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(private http: HttpClient) { }
  
  getAllCategories(){
    return this.http.get<MediaCategory[]>('https://localhost:7173/api/MediaCategories');
  }

  getAllSingers(){
    return this.http.get<Singer[]>('https://localhost:7173/getAllSingers');
  }

  getAllMovies(){
    return this.http.get<Movie[]>('https://localhost:7173/api/Movies/ClientsMovies/'+ sessionStorage.getItem("userId"));
  }

  deleteMovie(movieId:number){
    return this.http.delete<Movie>('https://localhost:7173/api/Movies/'+movieId);
  }

  addMovie(movie:Movie){
    return this.http.post<Movie>('https://localhost:7173/api/Movies',movie);
  }

  updateMovie(movie:Movie, movieId:number){
    return this.http.put<Movie>('https://localhost:7173/api/Movies/'+movieId, movie);
  }

  getAllTvShows(){
    return this.http.get<TvShow[]>('https://localhost:7173/getAllClientTvShows/'+ sessionStorage.getItem("userId"));
  }

  deleteTvShow(showId:number){
    return this.http.delete<TvShow>('https://localhost:7173/deleteTvShow/'+showId);
  }

  addTvShow(tvShow:TvShow){
    return this.http.post<TvShow>('https://localhost:7173/addTvShow',tvShow);
  }

  updateTvShow(show:TvShow, showId:number){
    return this.http.put<TvShow>('https://localhost:7173/updateTvShow/'+showId, show);
  }

  getAllSongs(){
    return this.http.get<Song[]>('https://localhost:7173/api/Songs/ClientSongs/'+ sessionStorage.getItem("userId"));
  }

  deleteSong(songId:number){
    return this.http.delete<Song>('https://localhost:7173/api/Songs/'+songId);
  }

  addSong(song:Song){
    return this.http.post<Song>('https://localhost:7173/api/Songs/AddSong/'+ sessionStorage.getItem("userId"),song);
  }

  updateSong(song:Song, songId:number){
    return this.http.put<Song>('https://localhost:7173/api/Songs/UpdateSong/'+songId, song);
  }

}
