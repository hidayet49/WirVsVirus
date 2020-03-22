import { Component, OnInit } from '@angular/core';
import { Questionaire } from 'src/app/models/questionaire';
import { Router } from '@angular/router';
import { PatientSignUp } from '../../models/patient-sign-up.model';
import { AlertService } from 'src/app/services/alert.service';
import { MedicalInstituteSignUpService } from 'src/app/modules/medical-institute/services/medical-institute-sign-up.service';
import { PatientSignUpService } from '../../services/patient-sign-up.service';
import { QuestionaireStateService } from '../../services/questionaire-state.service';

@Component({
  selector: 'app-patient-signup',
  templateUrl: './patient-signup.component.html',
  styleUrls: ['./patient-signup.component.css']
})
export class PatientSignupComponent implements OnInit {
  private questionaire: Questionaire;

  model: PatientSignUp = {
    address: {}
  };
  isRequesting: boolean;

  constructor( private questionaireStateService: QuestionaireStateService,
    private router: Router,
    private signUpService: PatientSignUpService,
    private alertService: AlertService) { }

  ngOnInit() {
    this.questionaire = this.questionaireStateService.questionaire;
    if (!this.questionaire || this.questionaire.ageCategory < 0) {
      //this.router.navigate(['/questionaire']);
    }
  }

  signUp() {
    this.isRequesting = true;
    this.signUpService.createUser(this.model)
      .subscribe(
        data => {
          this.isRequesting = false;
          this.router.navigate(['/email-confirmation-sent'], { state: { email: this.model.email } });
          this.alertService.success('Ihre Anmeldung war erfolgreich. Bitte bestätigen Sie noch Ihre Email-Adresse.');
        },
        error => {
          this.isRequesting = false;
          this.alertService.error(error);
        }
      );
  }
}
