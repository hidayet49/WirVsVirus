import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemTraveledComponent } from './item-traveled.component';

describe('ItemTraveledComponent', () => {
  let component: ItemTraveledComponent;
  let fixture: ComponentFixture<ItemTraveledComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemTraveledComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemTraveledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
