import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-smoking-situation',
  templateUrl: './item-smoking-situation.component.html',
  styleUrls: ['./item-smoking-situation.component.css']
})
export class ItemSmokingSituationComponent implements OnInit {
  @Input()
  public itemValue = -1;

  @Output()
  public itemChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

  onItemChange(id) {
    this.itemChanged.next(id);
  }
}
