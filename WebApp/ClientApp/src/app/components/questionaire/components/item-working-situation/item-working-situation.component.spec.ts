import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemWorkingSituationComponent } from './item-working-situation.component';

describe('ItemWorkingSituationComponent', () => {
  let component: ItemWorkingSituationComponent;
  let fixture: ComponentFixture<ItemWorkingSituationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemWorkingSituationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemWorkingSituationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
