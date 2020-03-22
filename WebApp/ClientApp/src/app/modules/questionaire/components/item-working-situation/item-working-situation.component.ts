import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-working-situation',
  templateUrl: './item-working-situation.component.html',
  styleUrls: ['./item-working-situation.component.css']
})
export class ItemWorkingSituationComponent implements OnInit {
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
