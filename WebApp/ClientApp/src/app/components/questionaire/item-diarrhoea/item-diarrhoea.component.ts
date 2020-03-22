import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-item-diarrhoea',
  templateUrl: './item-diarrhoea.component.html',
  styleUrls: ['./item-diarrhoea.component.css']
})
export class ItemDiarrhoeaComponent implements OnInit {
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
