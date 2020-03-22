import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-foreign-countries',
  templateUrl: './item-foreign-countries.component.html',
  styleUrls: ['./item-foreign-countries.component.css']
})
export class ItemForeignCountriesComponent implements OnInit {
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
