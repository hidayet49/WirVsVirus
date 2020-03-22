import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-sore-throat',
  templateUrl: './item-sore-throat.component.html',
  styleUrls: ['./item-sore-throat.component.css']
})
export class ItemSoreThroatComponent implements OnInit {
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
