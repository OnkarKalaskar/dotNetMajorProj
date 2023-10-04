import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvshowdialogComponent } from './tvshowdialog.component';

describe('TvshowdialogComponent', () => {
  let component: TvshowdialogComponent;
  let fixture: ComponentFixture<TvshowdialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvshowdialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvshowdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
