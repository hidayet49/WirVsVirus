import { Component, OnInit } from '@angular/core';
import { Questionaire } from 'src/app/models/questionaire';
import { Router } from '@angular/router';
import { QuestionaireStateService } from 'src/app/services/questionaire-state.service';

@Component({
  selector: 'app-questionaire',
  templateUrl: './questionaire.component.html',
  styleUrls: ['./questionaire.component.css']
})
export class QuestionaireComponent implements OnInit {

  public item = 0;
  public questionaire: Questionaire;
  private itemCount = 15;

  constructor(private router: Router,
    private questionaireStateService: QuestionaireStateService) {
      this.questionaireStateService.questionaire = new Questionaire();
      this.questionaire = this.questionaireStateService.questionaire;
     }

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

  submit() {
    this.router.navigate(['/questionaire/summary']);
  }
}
