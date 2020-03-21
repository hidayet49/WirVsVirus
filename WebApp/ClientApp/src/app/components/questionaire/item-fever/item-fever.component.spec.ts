import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemFeverComponent } from './item-fever.component';

describe('ItemFeverComponent', () => {
  let component: ItemFeverComponent;
  let fixture: ComponentFixture<ItemFeverComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemFeverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemFeverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
