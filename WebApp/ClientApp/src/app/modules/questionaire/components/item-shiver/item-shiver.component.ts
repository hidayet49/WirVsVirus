import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-shiver',
  templateUrl: './item-shiver.component.html',
  styleUrls: ['./item-shiver.component.css']
})
export class ItemShiverComponent implements OnInit {
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
