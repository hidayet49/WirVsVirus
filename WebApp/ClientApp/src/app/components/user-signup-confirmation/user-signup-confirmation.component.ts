import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PatientSignUpService } from 'src/app/modules/questionaire/services/patient-sign-up.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'user-signup-confirmation',
  templateUrl: './user-signup-confirmation.component.html',
  styleUrls: ['./user-signup-confirmation.component.css']
})
export class UserSignupConfirmationComponent implements OnInit {

  isRequesting = false;
  userId: string;
  token: string;
  urlSubscription: any;
  message = '';
  error = false;

  constructor(private route: ActivatedRoute,
    private signUpService: PatientSignUpService) { }

  ngOnInit() {
    this.urlSubscription = this.route.queryParams.subscribe(
      params => {
        this.userId = params['email'];
        this.token = encodeURIComponent(params['token']);
        this.confirmEmail();
      }
    );
  }

  confirmEmail() {
    this.isRequesting = true;
    this.signUpService.confirmEmail(this.userId, this.token)
    .subscribe(
      () => {
        this.isRequesting = false;
        this.message = 'Die Bestätigung ihrer Email-Adresse war erfolgreich.';
      },
      error => {
        this.error = true;
        this.isRequesting = false;
        this.message = error.error[0].description;
      }
    );
  }
}
