import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvshowcontainerComponent } from './tvshowcontainer.component';

describe('TvshowcontainerComponent', () => {
  let component: TvshowcontainerComponent;
  let fixture: ComponentFixture<TvshowcontainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvshowcontainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvshowcontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
