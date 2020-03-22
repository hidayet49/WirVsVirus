import { Component, OnInit } from '@angular/core';
import { MedicalInstituteSignUp } from '../../models/medical-institute-sign-up.model';
import { Router } from '@angular/router';
import { MedicalInstituteSignUpService } from '../../services/medical-institute-sign-up.service';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';
import { AddressService } from 'src/app/services/address.service';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { Address } from 'src/app/models/address.model';
import { BottomSheetDialogComponent } from 'src/app/modules/custom-angular-material/components/bottom-sheet-dialog/bottom-sheet-dialog.component';

@Component({
  selector: 'app-medical-institute-sign-up',
  templateUrl: './medical-institute-sign-up.component.html',
  styleUrls: ['./medical-institute-sign-up.component.css']
})
export class MedicalInstituteSignUpComponent implements OnInit {
  model: MedicalInstituteSignUp = {
    address: {}
  };
  isRequesting: boolean;

  constructor(
    private router: Router,
    private authService: AuthService,
    private signUpService: MedicalInstituteSignUpService,
    private addressService: AddressService,
    private bottomSheet: MatBottomSheet,
    private alertService: AlertService) {
  }

  ngOnInit() {
    this.authService.logout();
  }

  onSaveClick() {
    this.isRequesting = true;
    this.addressService.suggestAddresses(this.model.address).subscribe(
      data => {
        this.isRequesting = false;
        this.showDialogToConfirmAddress(data[0]);
      },
      error => {
        this.isRequesting = false;
        this.alertService.error(error);
      });
  }

  private showDialogToConfirmAddress(address: Address) {
    const dialogRef = this.bottomSheet.open(BottomSheetDialogComponent, {
      data: {
        title: 'Adresse prüfen',
        text: `Wir haben folgende Addresse gefunden: ${address.streetAndNumber}, ${address.zipCode} ${address.city}. Stimmt das?`,
        declineText: 'ABBRECHEN',
        acceptText: 'BESTÄTIGEN'
      }
    });
    dialogRef.afterDismissed().subscribe(result => {
      if (result) {
        this.model.address = address;
        this.signUp();
      }
    });
  }

  signUp() {
    this.isRequesting = true;
    this.signUpService.createUser(this.model)
      .subscribe(
        data => {
          this.isRequesting = false;
          // TODO redirect to other page than home
          this.router.navigate(['/']);
          this.alertService.success('Ihre Anmeldung war erfolgreich. Bitte bestätigen Sie noch Ihre Email-Adresse.');
        },
        error => {
          this.isRequesting = false;
          this.alertService.error(error);
        }
      );
  }

}
