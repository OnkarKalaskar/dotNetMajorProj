import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Movie } from 'src/app/movie/Model/Movie';
import { UserService } from 'src/app/role/user.service';
import { FavSong } from '../model/FavSong';
import { SongService } from '../song.service';

@Component({
  selector: 'app-songdetails',
  templateUrl: './songdetails.component.html',
  styleUrls: ['./songdetails.component.css']
})              
export class SongdetailsComponent implements OnInit{

  currentSong : any;
  allSongsData : any;
  songId : number = 0;
  moviesOfSameGenre : Movie[]=[];

  isFavorite : boolean = false;
  showToaster : boolean = false;

  constructor(private route : ActivatedRoute, private songService : SongService, private router : Router, private userService:UserService, private toastr : ToastrService){}

  ngOnInit(): void {
      this.getCurrentSongId();
      this.getCurrentSong();
  }

  getCurrentSongId()
  {
    this.route.params.subscribe((params: Params) =>{
      this.songId = params['id'];
    })
  }

  getCurrentSong()
  {
    this.songService.getSongById(this.songId).subscribe({
      next : (res) => {
        this.currentSong = res;
        
      },
      error : (err)=>{
        this.router.navigate(["unauthorized"]); 
        
      }
    })
  }

  //increase like
  increaseLike()
  {
    this.songService.increaseSongLike(this.currentSong.songId).subscribe({
      next : (res) =>{

      }
    });
    this.refresh();
  }

  //decrease like
  decreaseLike()
  {
    this.songService.decreaseSongLike(this.currentSong.songId).subscribe({
      next : (res) => {

      }
    });
    this.refresh();
  }

  playSong(id : number)
  {
    this.router.navigate(["songs/songdetails/"+id]);
  }

  refresh()
  {
    window.location.reload();
    //this.getAllMoviesData();
  }

  addSongToUserFavorites(songId : number)
  {
    let userId : any = sessionStorage.getItem("userId");

    let favSongObj : FavSong = {
      userId : userId,
      songId : songId
    }

    this.songService.addFavoriteSong(favSongObj).subscribe({
      next : (res : any) =>{
        if(res.message == 'ADDEDTOFAV')
        {
          this.toastr.success(" Added to favorites");
        }
        else if(res.message == "ALREADYINFAV")
        {
          this.toastr.success("Already in favorites");
        }
      }
    })
  }

}
