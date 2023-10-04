import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/movie/Model/Movie';
import { ClientsService } from '../clients.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import { EditdialogComponent } from '../editdialog/editdialog.component';
import { TvShow } from 'src/app/tvshow/model/tvshow';
import { TvshowdialogComponent } from '../tvshowdialog/tvshowdialog.component';
import { TvshoweditdialogComponent } from '../tvshoweditdialog/tvshoweditdialog.component';
import { Song } from 'src/app/song/model/song';
import { SongdialogComponent } from '../songdialog/songdialog.component';
import { SongeditdialogComponent } from '../songeditdialog/songeditdialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent implements OnInit {
  panelOpenState = false;
  movies:Movie[] = [];
  tvShows:TvShow[] = [];
  songs:Song[] = [];

  constructor(private clientService:ClientsService, private dialog:MatDialog, private editDialog:MatDialog, private tvshowDialog:MatDialog, private tvshoweditDialog:MatDialog, private songDialog:MatDialog, private songeditDialog: MatDialog){ }

  ngOnInit(): void {
      this.getAllMovies();
      this.getAllTvShows();
      this.getAllSongs();
  }

  getAllMovies(){
    this.clientService.getAllMovies().subscribe((data)=>{
      this.movies=data;
    })
  }

  getAllTvShows(){
    this.clientService.getAllTvShows().subscribe((data)=>{
      this.tvShows=data;
    })
  }

  getAllSongs(){
    this.clientService.getAllSongs().subscribe((data)=>{
      this.songs=data;
    })
  }

  deleteMovie(movieId:number){
    this.clientService.deleteMovie(movieId)
    .subscribe({
      next:(data)=>{
        window.location.reload();
      },
      error:(er)=>{
        console.log(er);
      }
    })
  }

  deleteTvShow(showId:number){
    this.clientService.deleteTvShow(showId)
    .subscribe({
      next:(data)=>{
        window.location.reload();
      },
      error:(er)=>{
        console.log(er);
      }
    })
  }

  deleteSong(songId:number){
    this.clientService.deleteSong(songId)
    .subscribe({
      next:(data)=>{
        window.location.reload();
      },
      error:(er)=>{
        console.log(er);
      }
    })
  }

  openDialog(){
    this.dialog.open(DialogComponent,{
      width: '60%',
      height: '600px',
      data:{
        title:'Movie Details'
      }
    })
  }

  openEditDialog(movie:Movie){
    this.editDialog.open(EditdialogComponent,{
      width: '60%',
      height: '600px',
      data:{
        title:'Movie Details',
        movie: movie
      }
    })
  }

  openTvShowDialog(){
    this.tvshowDialog.open(TvshowdialogComponent,{
      width: '60%',
      height: '600px',
      data:{
        title:'TvShow Details'
      }
    })
  }

  openTvShowEditDialog(tvShow:TvShow){
    this.tvshoweditDialog.open(TvshoweditdialogComponent,{
      width: '60%',
      height: '600px',
      data:{
        title:'TvShow Details',
        tvshow: tvShow
      }
    })
  }

  openSongDialog(){
    this.songDialog.open(SongdialogComponent,{
      width: '60%',
      height: '650px',
      data:{
        title:'Song Details'
      }
    })
  }

  openSongEditDialog(song:Song){
    this.songeditDialog.open(SongeditdialogComponent,{
      width: '60%',
      height: '650px',
      data:{
        title:'Song Details',
        song: song
      }
    })
  }

}