import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvshowdetailsComponent } from './tvshowdetails.component';

describe('TvshowdetailsComponent', () => {
  let component: TvshowdetailsComponent;
  let fixture: ComponentFixture<TvshowdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvshowdetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvshowdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
