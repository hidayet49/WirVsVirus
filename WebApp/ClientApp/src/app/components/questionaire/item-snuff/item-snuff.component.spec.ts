import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemSnuffComponent } from './item-snuff.component';

describe('ItemSnuffComponent', () => {
  let component: ItemSnuffComponent;
  let fixture: ComponentFixture<ItemSnuffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemSnuffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemSnuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
