import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemResidentialSituationComponent } from './item-residential-situation.component';

describe('ItemResidentialSituationComponent', () => {
  let component: ItemResidentialSituationComponent;
  let fixture: ComponentFixture<ItemResidentialSituationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemResidentialSituationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemResidentialSituationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
