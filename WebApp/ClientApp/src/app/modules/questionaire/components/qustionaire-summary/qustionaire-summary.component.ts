import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Questionaire } from 'src/app/models/questionaire';
import { QuestionaireStateService } from 'src/app/modules/questionaire/services/questionaire-state.service';

@Component({
  selector: 'app-qustionaire-summary',
  templateUrl: './qustionaire-summary.component.html',
  styleUrls: ['./qustionaire-summary.component.css']
})
export class QustionaireSummaryComponent implements OnInit {

  private questionaire: Questionaire;

  constructor(private questionaireStateService: QuestionaireStateService,
    private router: Router) { }

  ngOnInit() {
    this.questionaire = this.questionaireStateService.questionaire;

    if (!this.questionaire || this.questionaire.ageCategory < 0) {
      this.router.navigate(['/questionaire']);
    }

    if (this.questionaire.ageCategory > 4
      && this.questionaire.fever === 1
      && this.questionaire.contactToPositives === 1) {
      this.router.navigate(['/questionaire/signup']);
    }
  }

}
