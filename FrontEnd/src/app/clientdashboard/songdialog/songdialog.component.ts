import { Component, Inject,OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';
import { Song } from 'src/app/song/model/song';
import { Singer } from 'src/app/singer/model/singer';

@Component({
  selector: 'app-songdialog',
  templateUrl: './songdialog.component.html',
  styleUrls: ['./songdialog.component.css']
})

export class SongdialogComponent implements OnInit{

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

  singers: Singer[] = [];

  selectedSingerId:number=0;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<SongdialogComponent>, public clientService:ClientsService){ }

  ngOnInit(): void {
    this.inputdata= this.data;
    this.getAllCategories();
    this.getAllSingers();
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

  getAllSingers(){
    this.clientService.getAllSingers().subscribe((data)=>{
      this.singers=data;
    })
  }

  closeDialog(){
    this.ref.close();
  }

  addedSingers:string[]=[];

  s:any='';

  AddSinger(singerId:number){
    this.songForm.singers.push(singerId);
    //this.addedSingers.push(this.singers.find(x => x.singerId == singerId)?.singerName)
    this.s=this.singers.find(x => x.singerId == singerId)?.singerName;
    if(this.s != undefined){
      this.addedSingers.push(this.s);
    }
  }

  addSong(){
    this.songForm.songPath= this.tempSongAudio.substring(12);
    this.songForm.songPoster= this.tempSongPoster.substring(12);
    this.songForm.categoryId= this.selectedCategoryId;
    this.songForm.songType= this.selectedSongType;
    this.songForm.songGeneration= this.selectedSongGeneration;

    this.clientService.addSong(this.songForm)
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