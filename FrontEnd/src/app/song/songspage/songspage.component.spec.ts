import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongspageComponent } from './songspage.component';

describe('SongspageComponent', () => {
  let component: SongspageComponent;
  let fixture: ComponentFixture<SongspageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SongspageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongspageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
