import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemDiarrhoeaComponent } from './item-diarrhoea.component';

describe('ItemDiarrhoeaComponent', () => {
  let component: ItemDiarrhoeaComponent;
  let fixture: ComponentFixture<ItemDiarrhoeaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemDiarrhoeaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemDiarrhoeaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
