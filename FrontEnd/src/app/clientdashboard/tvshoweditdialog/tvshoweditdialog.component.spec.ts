import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TvshoweditdialogComponent } from './tvshoweditdialog.component';

describe('TvshoweditdialogComponent', () => {
  let component: TvshoweditdialogComponent;
  let fixture: ComponentFixture<TvshoweditdialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TvshoweditdialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TvshoweditdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
