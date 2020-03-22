import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemHeinsbergComponent } from './item-heinsberg.component';

describe('ItemHeinsbergComponent', () => {
  let component: ItemHeinsbergComponent;
  let fixture: ComponentFixture<ItemHeinsbergComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemHeinsbergComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemHeinsbergComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
