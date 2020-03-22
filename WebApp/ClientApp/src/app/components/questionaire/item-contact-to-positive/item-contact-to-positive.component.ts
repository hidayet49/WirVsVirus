import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-contact-to-positive',
  templateUrl: './item-contact-to-positive.component.html',
  styleUrls: ['./item-contact-to-positive.component.css']
})
export class ItemContactToPositiveComponent implements OnInit {
  @Input()
  public itemValue = -1;

  @Input()
  public text = '';

  @Output()
  public itemChanged = new EventEmitter<number>();

  @Output()
  public textChanged = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  onItemChange(id) {
    if (id === 2) {
      this.text = '';
      this.textChanged.next('');
    }
    this.itemChanged.next(id);
  }

  modelChanged(text){
    this.textChanged.next(text);
  }
}
