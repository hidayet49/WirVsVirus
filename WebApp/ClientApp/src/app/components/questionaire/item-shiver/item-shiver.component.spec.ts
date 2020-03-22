import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemShiverComponent } from './item-shiver.component';

describe('ItemShiverComponent', () => {
  let component: ItemShiverComponent;
  let fixture: ComponentFixture<ItemShiverComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemShiverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemShiverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
