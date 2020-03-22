import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemAgeComponent } from './item-age.component';

describe('ItemAgeComponent', () => {
  let component: ItemAgeComponent;
  let fixture: ComponentFixture<ItemAgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemAgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemAgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
