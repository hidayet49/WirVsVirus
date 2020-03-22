import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-aching-limbs',
  templateUrl: './item-aching-limbs.component.html',
  styleUrls: ['./item-aching-limbs.component.css']
})
export class ItemAchingLimbsComponent implements OnInit {
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
