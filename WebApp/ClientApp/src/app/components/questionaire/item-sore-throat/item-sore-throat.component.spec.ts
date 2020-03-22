import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemSoreThroatComponent } from './item-sore-throat.component';

describe('ItemSoreThroatComponent', () => {
  let component: ItemSoreThroatComponent;
  let fixture: ComponentFixture<ItemSoreThroatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemSoreThroatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemSoreThroatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
