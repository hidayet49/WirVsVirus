import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemContactToPositiveComponent } from './item-contact-to-positive.component';

describe('ItemContactToPositiveComponent', () => {
  let component: ItemContactToPositiveComponent;
  let fixture: ComponentFixture<ItemContactToPositiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemContactToPositiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemContactToPositiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
