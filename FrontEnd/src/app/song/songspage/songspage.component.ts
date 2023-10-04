import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';
import { Song } from '../model/song';
import { SongService } from '../song.service';
@Component({
  selector: 'app-songspage',
  templateUrl: './songspage.component.html',
  styleUrls: ['./songspage.component.css']
})
export class SongspageComponent implements OnInit {

  allSongs : Song[] = [];
  songCategories: MediaCategory[] = [];
  songCategoriesId: number[] = [];
  
  //song category wise array
  Pop : Song[] = [];
  Rock : Song[] = [];
  Folk : Song[] = [];
  Bollywood : Song[] = [];

  constructor(private songService : SongService, private router :  Router){}

  ngOnInit(): void {
      this.getSongs();
  }


  checkUserLogin(songId: number) {
    let token = sessionStorage.getItem("access_token");

    if (token != null) {
      this.router.navigate(['songs/songdetails/' + songId]);
    }
    else {
      this.router.navigate(['login']);
    }
  }
  
  getSongs()
  {
    this.songService.getAllSongs().subscribe({
      next : (res)=>{
        this.allSongs = res;
        res.forEach((song : Song)=>{
          if(song.categoryId ==8)
          {
            this.Pop.push(song);
            this.Pop.push(song);
          }
          if(song.categoryId ==9)
          {
            this.Rock.push(song);
            this.Rock.push(song);
          }
          if(song.categoryId ==10)
          {
            this.Folk.push(song);
            this.Folk.push(song);
          }
          if(song.categoryId ==12)
          {
            this.Bollywood.push(song);
            this.Bollywood.push(song);
          }
        })
      }
    })
  }

  getSongCategories(){

    this.songService.getAllMediaCategories().subscribe({
      next : (res) =>{
        res.forEach((element: any) => {
          if (element.categoryId > 7) {
            this.songCategoriesId.push(element.categoryId);
            this.songCategories.push(element);
          }
          });
        }
    })
  }    
}
