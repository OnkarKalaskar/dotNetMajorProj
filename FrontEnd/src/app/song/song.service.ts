import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http : HttpClient) { }

  getAllSongs() : Observable<any>{
    return this.http.get('https://localhost:7173/getAllSongs');
  }

  getSongById(id : number)
  {
    return this.http.get('https://localhost:7173/api/Songs/'+id);
  }

  increaseSongLike(id : number)
  {
    return this.http.patch('https://localhost:7173/api/Songs/IncreaseLike?songId='+id,'')
  }

  decreaseSongLike(id : number)
  {
    return this.http.patch('https://localhost:7173/api/Songs/DecreaseLike?songId='+id,'');
  }

  addFavoriteSong(payload : any)
  {
    return this.http.post('https://localhost:7173/api/Songs/AddFavSong',payload);
  }

  removeFavoriteSong(payload : any)
  {
    return this.http.post('https://localhost:7173/api/Songs/RemoveFromFavSong',payload)
  }

  getAllMediaCategories() : Observable<any>
  {
    return this.http.get('https://localhost:7173/api/MediaCategories');
  }
}
