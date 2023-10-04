import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongcontainerComponent } from './songcontainer.component';

describe('SongcontainerComponent', () => {
  let component: SongcontainerComponent;
  let fixture: ComponentFixture<SongcontainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SongcontainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongcontainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
