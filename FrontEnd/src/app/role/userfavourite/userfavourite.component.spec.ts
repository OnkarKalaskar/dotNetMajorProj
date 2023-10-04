import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserfavouriteComponent } from './userfavourite.component';

describe('UserfavouriteComponent', () => {
  let component: UserfavouriteComponent;
  let fixture: ComponentFixture<UserfavouriteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserfavouriteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserfavouriteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
