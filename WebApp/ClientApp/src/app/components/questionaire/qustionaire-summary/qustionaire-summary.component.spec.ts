import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QustionaireSummaryComponent } from './qustionaire-summary.component';

describe('QustionaireSummaryComponent', () => {
  let component: QustionaireSummaryComponent;
  let fixture: ComponentFixture<QustionaireSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QustionaireSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QustionaireSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
