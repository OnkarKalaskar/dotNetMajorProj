import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongeditdialogComponent } from './songeditdialog.component';

describe('SongeditdialogComponent', () => {
  let component: SongeditdialogComponent;
  let fixture: ComponentFixture<SongeditdialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SongeditdialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongeditdialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
