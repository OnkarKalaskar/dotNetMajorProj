import { Component, Inject,OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { Movie } from 'src/app/movie/Model/Movie';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})

export class DialogComponent implements OnInit{

  inputdata:any;
  tempMovieVideo:string='';
  tempMoviePoster:string='';

  movieForm: Movie={
    movieId: 0,
    movieName: '',
    moviePath: '',
    moviePoster: '',
    movieDescription: '',
    likes: 0,
    userId: Number(sessionStorage.getItem("userId")),
    categoryId: 0,
    categoryName: ''
  }

  categories:MediaCategory[] = [];

  selectedCategoryId: number=0;

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<DialogComponent>, public clientService:ClientsService){ }

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

  addMovie(){
    this.movieForm.moviePath= this.tempMovieVideo.substring(12);
    this.movieForm.moviePoster= this.tempMoviePoster.substring(12);
    this.movieForm.categoryId= this.selectedCategoryId;

    this.clientService.addMovie(this.movieForm)
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