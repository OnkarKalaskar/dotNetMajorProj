import { Component, Inject,OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';
import { Song } from 'src/app/song/model/song';
import { Singer } from 'src/app/singer/model/singer';

@Component({
  selector: 'app-songeditdialog',
  templateUrl: './songeditdialog.component.html',
  styleUrls: ['./songeditdialog.component.css']
})
export class SongeditdialogComponent implements OnInit{

  inputdata:any;
  tempSongAudio:string='';
  tempSongPoster:string='';
  selectedSongType:string='';
  selectedSongGeneration:string='';

  songTypes:any[]=[{typeName:"new"},{typeName:"old"}];
  songGenerations:any[]=[{genName:"1970"},{genName:"1980"},{genName:"1990"},{genName:"2000"},{genName:"2010"},{genName:"2020"},{genName:"2030"}];

  songForm: Song={
    songId: 0,
    songName: '',
    songPath: '',
    songPoster: '',
    songLyrics: '',
    songGeneration: '',
    songType: '',
    songDescription: '',
    likes: 0,
    userId: Number(sessionStorage.getItem("userId")),
    categoryId: 0,
    singers:[]
  }
  categories:MediaCategory[] = [];

  selectedCategoryId: number=0;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<SongeditdialogComponent>, public clientService:ClientsService){ }

  ngOnInit(): void {
    this.inputdata= this.data;
    this.getAllCategories();
    this.initializeSongData();
  }

  initializeSongData(){
    this.songForm.songId= this.inputdata.song.songId;
    this.songForm.songName= this.inputdata.song.songName;
    this.songForm.songDescription= this.inputdata.song.songDescription;
    this.songForm.songLyrics= this.inputdata.song.songLyrics;
    this.songForm.categoryId= this.inputdata.song.categoryId;
    this.songForm.songGeneration= this.inputdata.song.songGeneration;
    this.songForm.songType= this.inputdata.song.songType;
    this.songForm.likes= this.inputdata.song.likes;
  }

  getAllCategories(){
    this.clientService.getAllCategories().subscribe((data)=>{
      data.forEach((ele)=> {
        if(ele.categoryId > 7){
          this.categories.push(ele);
        }
      })
    })
  }

  closeDialog(){
    this.ref.close();
  }

  editSong(){

    if(this.tempSongAudio.length != 0){
      this.songForm.songPath= this.tempSongAudio.substring(12);
    }else{
      this.songForm.songPath= this.inputdata.song.songPath;
    }
    
    if(this.tempSongPoster.length != 0){
      this.songForm.songPoster= this.tempSongPoster.substring(12);
    }else{
      this.songForm.songPoster= this.inputdata.song.songPoster;
    }
    
    if(this.selectedCategoryId !=0){
      this.songForm.categoryId= this.selectedCategoryId;
    }else{
      this.songForm.categoryId= this.inputdata.song.categoryId;
    }

    if(this.selectedSongGeneration.length != 0){
      this.songForm.songGeneration= this.selectedSongGeneration;
    }else{
      this.songForm.songGeneration= this.inputdata.song.songGeneration;
    }

    if(this.selectedSongType.length != 0){
      this.songForm.songType= this.selectedSongType;
    }else{
      this.songForm.songType= this.inputdata.song.songType;
    }

    console.log(this.songForm);

    this.clientService.updateSong(this.songForm, this.songForm.songId)
    .subscribe({
      next:(data)=>{
        window.location.reload();
      },
      error:(er)=>{
        console.log(er);
      }
    })
  }
}
