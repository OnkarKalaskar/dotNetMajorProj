import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { User } from './model/user';
import { UserLogin } from './model/user-login';
import { UserLogged } from './model/userLogged.model';
import { Router } from '@angular/router';
import { UserUpdate } from './model/updateUser';
import { FormGroup } from '@angular/forms';
import { FavMovie } from '../movie/Model/FavMovie';
import { RequestInterceptor } from '../request.interceptor';
@Injectable({
  providedIn: 'root',
})
export class UserService {

  tokenresp: any;
  private _updatemenu = new Subject<void>();
  searchKey:string='';

  constructor(private http: HttpClient, private router: Router) { }

  get updateMenu() {
    return this._updatemenu;
  }
  register(user: User): Observable<any> {
    return this.http.post('https://localhost:7173/register', user);
  }

  login(user: UserLogin): Observable<any> {
    return this.http.post('https://localhost:7173/login', user);
  }

  setToken(token: any) {
    sessionStorage.setItem('access_token', token);
  }

  getToken() {
    return sessionStorage.getItem('access_token');
  }

  getRoleByToken(token: any) {
    if (token != null) {
      let _token = token.split('.')[1];
      this.tokenresp = JSON.parse(atob(_token));
      return this.tokenresp.role;
    }
    return null;
  }

  removeToken() {
    sessionStorage.removeItem('access_token');
    sessionStorage.removeItem('userId');
  }

  getAllUsers(): Observable<any> {
    return this.http.get('https://localhost:7173/getUsers');
  }

  getAllClients(): Observable<any> {
    return this.http.get('https://localhost:7173/getClient');
  }

  approveClient(id: string): Observable<any> {
    return this.http.patch('https://localhost:7173/approveClient/' + id, id);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete('https://localhost:7173/deleteUser/' + id)
  }

  getUserFavouritesTvShows(id: string): Observable<any> {
    return this.http.get('https://localhost:7173/getUserFav/' + id)
  }

  getUserFavMovies(id: string): Observable<any> {
    return this.http.get('https://localhost:7173/api/Movies/GetFavMovies/' + id)
  }
  getUserFavSongs(id: string): Observable<any> {
    return this.http.get('https://localhost:7173/api/Songs/GetFavSongs/' + id)
  }

  getUserDataById(id: string): Observable<any> {
    return this.http.get('https://localhost:7173/getUser/' + id)
  }

  updateUser(id: string, payload: UserUpdate): Observable<any> {
    return this.http.patch('https://localhost:7173/updateUser/' + id, payload);
  }

  updateUserProfilePic(id: string, payload: string): Observable<any> {
    // https://localhost:7173/updateProfilePic/1?pic=cdhgcgc
    return this.http.patch('https://localhost:7173/updateProfilePic/' + id + '?pic=' + payload, '');
  }

  changePassword(payload: UserLogin): Observable<any> {
    return this.http.patch('https://localhost:7173/forgotpassword', payload)
  }

  setSearchKey(key:string){
    this.searchKey= key;
  }

  getSearchKey():string{
    return this.searchKey;
  }

  getUserSearchedMovies(searchStr:string): Observable<any>{
    return this.http.get('https://localhost:7173/api/Movies/GetSearchedMovies/'+searchStr);
    
  }

  getUserSearchedTvShows(searchStr:string): Observable<any>{
    return this.http.get('https://localhost:7173/GetSearchedTvShows/'+searchStr);
  }

  getUserSearchedSongs(searchStr:string): Observable<any>{
    return this.http.get('https://localhost:7173/api/Songs/GetSearchedSongs/'+searchStr);
  }

  removeMovieFromFavorites(movieId:number, userId : number)
  {
    return this.http.delete('https://localhost:7173/api/Movies/RemoveFromFav?movieId='+movieId+'&userId='+userId);
  }

  removeTvShowFromFavorites(tvShowId : number, userId:number)
  {
    return this.http.delete('https://localhost:7173/removeFromFav?userId='+userId+'&tvShowId='+tvShowId);
  }

  removeSongFromFavorites(songId : number, userId: number)
  {
    return this.http.delete('https://localhost:7173/api/Songs/RemoveFromFavSong?userId='+userId+'&songId='+songId)
  }

}