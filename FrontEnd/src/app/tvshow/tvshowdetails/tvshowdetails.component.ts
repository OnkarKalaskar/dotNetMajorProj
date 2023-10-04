import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TvshowsService } from '../tvshows.service';
import { TvShow } from '../model/tvshow';
import { UserService } from 'src/app/role/user.service';
import { FavTvShow } from '../model/favtvshow';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tvshowdetails',
  templateUrl: './tvshowdetails.component.html',
  styleUrls: ['./tvshowdetails.component.css']
})
export class TvshowdetailsComponent implements OnInit {

  tvShowId: number = 0;
  currentTvShow: any
  constructor(private route: ActivatedRoute, private tvshowService: TvshowsService, private userService: UserService, private toaster: ToastrService) { }


  ngOnInit(): void {
    this.getCurrentTvShowId()
    this.getCurrentTvShow()
  }

  getCurrentTvShowId() {
    this.route.params.subscribe((params: Params) => {
      this.tvShowId = params['id'];

    })
  }

  getCurrentTvShow() {
    this.tvshowService.getTvShowById(this.tvShowId).subscribe({
      next: (res) => {
        this.currentTvShow = res
      }
    })
  }
// some changes in tv show details page
  addTvShowToFav(tvShowId: number) {

    let userId: any = sessionStorage.getItem("userId");
    let userFavTvShow: FavTvShow = {
      tvShowId: tvShowId,
      userId: userId
    }

    this.tvshowService.addToFav(userFavTvShow).subscribe({
      next: (res) => {
        if (res.message == 'ADDED') {
          this.toaster.success("Added to favourite")
        } else if (res.message == 'ALREADY ADDED') {
          this.toaster.success("Already added to favourite")
        }
      }
    })
  }

  increaseLike() {
    this.tvshowService.increaseLike(this.currentTvShow.tvShowId).subscribe({

    })
    this.refresh()
  }

  decreaseLike() {
    this.tvshowService.decreaseLike(this.currentTvShow.tvShowId).subscribe({

    })
    this.refresh()
  }

  refresh() {
    window.location.reload();
    //this.getAllMoviesData();
  }
}
