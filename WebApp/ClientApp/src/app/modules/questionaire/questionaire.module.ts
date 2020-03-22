import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QuestionaireComponent } from './questionaire.component';
import { ItemAgeComponent } from './components/item-age/item-age.component';
import { ItemResidentialSituationComponent } from './components/item-residential-situation/item-residential-situation.component';
import { ItemWorkingSituationComponent } from './components/item-working-situation/item-working-situation.component';
import { ItemSmokingSituationComponent } from './components/item-smoking-situation/item-smoking-situation.component';
import { ItemTraveledComponent } from './components/item-traveled/item-traveled.component';
import { ItemHeinsbergComponent } from './components/item-heinsberg/item-heinsberg.component';
import { FormsModule } from '@angular/forms';
import { ItemForeignCountriesComponent } from './components/item-foreign-countries/item-foreign-countries.component';
import { ItemContactToPositiveComponent } from './components/item-contact-to-positive/item-contact-to-positive.component';
import { ItemFeverComponent } from './components/item-fever/item-fever.component';
import { ItemShiverComponent } from './components/item-shiver/item-shiver.component';
import { ItemDefeatedComponent } from './components/item-defeated/item-defeated.component';
import { ItemAchingLimbsComponent } from './components/item-aching-limbs/item-aching-limbs.component';
import { ItemCouchComponent } from './components/item-couch/item-couch.component';
import { ItemSnuffComponent } from './components/item-snuff/item-snuff.component';
import { ItemDiarrhoeaComponent } from './components/item-diarrhoea/item-diarrhoea.component';
import { ItemSoreThroatComponent } from './components/item-sore-throat/item-sore-throat.component';
import { PatientSignupComponent } from './components/patient-signup/patient-signup.component';
import { QustionaireSummaryComponent } from './components/qustionaire-summary/qustionaire-summary.component';
import { QuestionaireStateService } from 'src/app/modules/questionaire/services/questionaire-state.service';
import { CustomAngularMaterialModule } from 'src/app/modules/custom-angular-material/custom-angular-material.module';
import { PatientSignUpService } from './services/patient-sign-up.service';

@NgModule({
  declarations: [
    QuestionaireComponent,
    ItemAgeComponent,
    ItemResidentialSituationComponent,
    ItemWorkingSituationComponent,
    ItemSmokingSituationComponent,
    ItemTraveledComponent,
    ItemHeinsbergComponent,
    ItemForeignCountriesComponent,
    ItemContactToPositiveComponent,
    ItemFeverComponent,
    ItemShiverComponent,
    ItemDefeatedComponent,
    ItemAchingLimbsComponent,
    ItemCouchComponent,
    ItemSnuffComponent,
    ItemDiarrhoeaComponent,
    ItemSoreThroatComponent,
    PatientSignupComponent,
    QustionaireSummaryComponent
  ],
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: QuestionaireComponent
      },
      {
        path: 'summary',
        component: QustionaireSummaryComponent
      },
      {
        path: 'signup',
        component: PatientSignupComponent
      },
    ]),
    CommonModule,
    FormsModule,
    CustomAngularMaterialModule
  ],
  providers: [
    QuestionaireStateService,
    PatientSignUpService,
  ],
})
export class QuestionaireModule { }
