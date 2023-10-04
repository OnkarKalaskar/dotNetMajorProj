import { Component, Inject,  OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';
import { TvShow } from 'src/app/tvshow/model/tvshow';

@Component({
  selector: 'app-tvshoweditdialog',
  templateUrl: './tvshoweditdialog.component.html',
  styleUrls: ['./tvshoweditdialog.component.css']
})

export class TvshoweditdialogComponent implements OnInit{

  inputdata:any;
  tempShowVideo:string='';
  tempShowPoster:string='';

  showForm: TvShow={
    tvShowId: 0,
    tvShowName: '',
    tvShowPath: '',
    tvShowPoster: '',
    tvShowDescription: '',
    likes: 0,
    userId: Number(sessionStorage.getItem("userId")),
    categoryId: 0,
    categoryName: ''
  }

  categories:MediaCategory[] = [];

  selectedCategoryId:number = 0;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<TvshoweditdialogComponent>, public clientService:ClientsService){ }

  ngOnInit(): void {
    this.inputdata= this.data;
    this.getAllCategories();
    this.initializeShowData();
  }

  initializeShowData(){
    this.showForm.tvShowId= this.inputdata.tvshow.tvShowId;
    this.showForm.tvShowName= this.inputdata.tvshow.tvShowName;
    this.showForm.tvShowDescription= this.inputdata.tvshow.tvShowDescription;
    this.showForm.categoryId= this.inputdata.tvshow.categoryId;
    this.showForm.likes= this.inputdata.tvshow.likes;
  }

  getAllCategories(){
    this.clientService.getAllCategories().subscribe((data)=>{
      data.forEach((ele)=> {
        if(ele.categoryId < 8){
          this.categories.push(ele);
        }
      })
    })
  }
 
  closeDialog(){
    this.ref.close();
  }

  editTvShow(){
    if(this.tempShowVideo.length != 0){
      this.showForm.tvShowPath= this.tempShowVideo.substring(12);
    }else{
      this.showForm.tvShowPath= this.inputdata.tvshow.tvShowPath;
    }
    
    if(this.tempShowPoster.length != 0){
      this.showForm.tvShowPoster= this.tempShowPoster.substring(12);
    }else{
      this.showForm.tvShowPoster= this.inputdata.tvshow.tvShowPoster;
    }
    
    if(this.selectedCategoryId !=0){
      this.showForm.categoryId= this.selectedCategoryId;
    }else{
      this.showForm.categoryId= this.inputdata.tvshow.categoryId;
    }   

    console.log(this.showForm.categoryId);
    this.clientService.updateTvShow( this.showForm,this.inputdata.tvshow.tvShowId)
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