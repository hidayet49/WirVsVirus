import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-traveled',
  templateUrl: './item-traveled.component.html',
  styleUrls: ['./item-traveled.component.css']
})
export class ItemTraveledComponent implements OnInit {
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
