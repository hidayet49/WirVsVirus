import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPassword } from 'src/app/models/reset-password.model';
import { AlertService } from 'src/app/services/alert.service';
import { DriverSignUpService } from 'src/app/services/driver-sign-up.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit, OnDestroy {
  model: ResetPassword = {};
  urlSubscription: any;
  isProcessing = false;
  text: string;

  constructor(private route: ActivatedRoute,
    private signUpService: DriverSignUpService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.urlSubscription = this.route.queryParams.subscribe(
      params => {
        this.model.email = params['email'];
        this.model.token = encodeURIComponent(params['token']);
      }
    );
  }

  public resetPassword() {
    this.isProcessing = true;
    this.signUpService.resetPassword(this.model).subscribe(
      data => {
        this.isProcessing = false;
        this.alertService.success('Dein Passwort wurde erfolgreich geändert! Bitte logge dich nun damit ein.');
          // this.router.navigate(['/login']);
      },
      error => {
        this.alertService.error(error);
        this.isProcessing = false;
      }
    );
  }

  ngOnDestroy(): void {
  }

}
