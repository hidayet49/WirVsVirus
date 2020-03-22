import { Component, OnInit } from '@angular/core';
import { Questionaire } from 'src/app/models/questionaire';
import { QuestionaireStateService } from 'src/app/services/questionaire-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-signup',
  templateUrl: './patient-signup.component.html',
  styleUrls: ['./patient-signup.component.css']
})
export class PatientSignupComponent implements OnInit {
  private questionaire: Questionaire;

  constructor( private questionaireStateService: QuestionaireStateService,
    private router: Router) { }

  ngOnInit() {
    this.questionaire = this.questionaireStateService.questionaire;
    if (!this.questionaire || this.questionaire.ageCategory < 0) {
      this.router.navigate(['/questionaire']);
    }
  }

}
