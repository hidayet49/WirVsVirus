import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemCouchComponent } from './item-couch.component';

describe('ItemCouchComponent', () => {
  let component: ItemCouchComponent;
  let fixture: ComponentFixture<ItemCouchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemCouchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemCouchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
