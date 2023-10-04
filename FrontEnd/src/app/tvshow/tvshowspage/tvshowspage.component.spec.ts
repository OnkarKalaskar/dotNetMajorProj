import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvshowspageComponent } from './tvshowspage.component';

describe('TvshowspageComponent', () => {
  let component: TvshowspageComponent;
  let fixture: ComponentFixture<TvshowspageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvshowspageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvshowspageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
