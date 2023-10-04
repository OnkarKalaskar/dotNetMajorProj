import { Component, Inject,  OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService } from '../clients.service';
import { Movie } from 'src/app/movie/Model/Movie';
import { MediaCategory } from 'src/app/movie/Model/MediaCategory';

@Component({
  selector: 'app-editdialog',
  templateUrl: './editdialog.component.html',
  styleUrls: ['./editdialog.component.css']
})

export class EditdialogComponent implements OnInit{

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

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,private ref:MatDialogRef<EditdialogComponent>, public clientService:ClientsService){ }

  ngOnInit(): void {
      this.inputdata= this.data;
      this.getAllCategories();
      this.initializeMovieData();
  }

  initializeMovieData(){
    this.movieForm.movieId= this.inputdata.movie.movieId;
    this.movieForm.movieName= this.inputdata.movie.movieName;
    this.movieForm.movieDescription= this.inputdata.movie.movieDescription;
    this.movieForm.categoryId= this.inputdata.movie.categoryId;
    this.movieForm.likes= this.inputdata.movie.likes;
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

  editMovie(){
    if(this.tempMovieVideo.length != 0){
      this.movieForm.moviePath= this.tempMovieVideo.substring(12);
    }else{
      this.movieForm.moviePath= this.inputdata.movie.moviePath;
    }
    
    if(this.tempMoviePoster.length != 0){
      this.movieForm.moviePoster= this.tempMoviePoster.substring(12);
    }else{
      this.movieForm.moviePoster= this.inputdata.movie.moviePoster;
    }
    
    if(this.selectedCategoryId !=0){
      this.movieForm.categoryId= this.selectedCategoryId;
    }else{
      this.movieForm.categoryId= this.inputdata.movie.categoryId;
    }      

    this.clientService.updateMovie( this.movieForm,this.inputdata.movie.movieId)
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
