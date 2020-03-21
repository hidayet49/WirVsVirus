import { Component, OnInit } from '@angular/core';
import { Questionaire } from 'src/app/models/questionaire';

@Component({
  selector: 'app-questionaire',
  templateUrl: './questionaire.component.html',
  styleUrls: ['./questionaire.component.css']
})
export class QuestionaireComponent implements OnInit {

  public item = 0;
  public questionaire = new Questionaire();
  private itemCount = 15;

  constructor() { }

  ngOnInit() {
  }

  next() {
    this.item++;
    if (this.item > this.itemCount) {
      this.item = this.itemCount;
    }
  }

  back() {
    this.item--;
    if (this.item < 0) {
      this.item = 0;
    }
  }
}
