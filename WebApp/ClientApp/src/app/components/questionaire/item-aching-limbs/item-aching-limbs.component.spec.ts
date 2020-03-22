import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemAchingLimbsComponent } from './item-aching-limbs.component';

describe('ItemAchingLimbsComponent', () => {
  let component: ItemAchingLimbsComponent;
  let fixture: ComponentFixture<ItemAchingLimbsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemAchingLimbsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemAchingLimbsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
