import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemSmokingSituationComponent } from './item-smoking-situation.component';

describe('ItemSmokingSituationComponent', () => {
  let component: ItemSmokingSituationComponent;
  let fixture: ComponentFixture<ItemSmokingSituationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemSmokingSituationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemSmokingSituationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
