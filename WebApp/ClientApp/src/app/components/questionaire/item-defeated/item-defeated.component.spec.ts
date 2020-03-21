import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemDefeatedComponent } from './item-defeated.component';

describe('ItemDefeatedComponent', () => {
  let component: ItemDefeatedComponent;
  let fixture: ComponentFixture<ItemDefeatedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemDefeatedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemDefeatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
