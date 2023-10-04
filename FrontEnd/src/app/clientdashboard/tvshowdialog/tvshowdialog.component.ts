import { Component, Inject,OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';
import { TvShow } from 'src/app/tvshow/model/tvshow';

@Component({
  selector: 'app-tvshowdialog',
  templateUrl: './tvshowdialog.component.html',
  styleUrls: ['./tvshowdialog.component.css']
})

export class TvshowdialogComponent implements OnInit{

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

  selectedCategoryId: number=0;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<TvshowdialogComponent>, public clientService:ClientsService){ }

  ngOnInit(): void {
      this.inputdata= this.data;
      this.getAllCategories();
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

  addTvShow(){
    this.showForm.tvShowPath= this.tempShowVideo.substring(12);
    this.showForm.tvShowPoster= this.tempShowPoster.substring(12);
    this.showForm.categoryId= this.selectedCategoryId;

    this.clientService.addTvShow(this.showForm)
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