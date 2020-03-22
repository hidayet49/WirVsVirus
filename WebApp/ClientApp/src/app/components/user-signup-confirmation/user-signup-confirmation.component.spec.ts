import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSignupConfirmationComponent } from './user-signup-confirmation.component';

describe('UserSignupConfirmationComponent', () => {
  let component: UserSignupConfirmationComponent;
  let fixture: ComponentFixture<UserSignupConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSignupConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSignupConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
