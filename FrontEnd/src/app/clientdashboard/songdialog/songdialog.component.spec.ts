import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongdialogComponent } from './songdialog.component';

describe('SongdialogComponent', () => {
  let component: SongdialogComponent;
  let fixture: ComponentFixture<SongdialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SongdialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
