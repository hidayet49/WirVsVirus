import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-heinsberg',
  templateUrl: './item-heinsberg.component.html',
  styleUrls: ['./item-heinsberg.component.css']
})
export class ItemHeinsbergComponent implements OnInit {
  @Input()
  public itemValue = -1;

  @Input()
  public returnDate = '';

  @Output()
  public itemChanged = new EventEmitter<number>();

  @Output()
  public textChanged = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  onItemChange(id) {
    if (id === 2) {
      this.returnDate = '';
      this.textChanged.next('');
    }
    this.itemChanged.next(id);
  }

  modelChanged(text){
    this.textChanged.next(text);
  }
}
