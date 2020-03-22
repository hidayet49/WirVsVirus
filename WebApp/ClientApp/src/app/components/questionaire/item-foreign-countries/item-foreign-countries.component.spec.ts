import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemForeignCountriesComponent } from './item-foreign-countries.component';

describe('ItemForeignCountriesComponent', () => {
  let component: ItemForeignCountriesComponent;
  let fixture: ComponentFixture<ItemForeignCountriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemForeignCountriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemForeignCountriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
