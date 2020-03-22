import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QuestionaireComponent } from './questionaire.component';
import { ItemAgeComponent } from './item-age/item-age.component';
import { ItemResidentialSituationComponent } from './item-residential-situation/item-residential-situation.component';
import { ItemWorkingSituationComponent } from './item-working-situation/item-working-situation.component';
import { ItemSmokingSituationComponent } from './item-smoking-situation/item-smoking-situation.component';
import { ItemTraveledComponent } from './item-traveled/item-traveled.component';
import { ItemHeinsbergComponent } from './item-heinsberg/item-heinsberg.component';
import { FormsModule } from '@angular/forms';
import { ItemForeignCountriesComponent } from './item-foreign-countries/item-foreign-countries.component';
import { ItemContactToPositiveComponent } from './item-contact-to-positive/item-contact-to-positive.component';
import { ItemFeverComponent } from './item-fever/item-fever.component';
import { ItemShiverComponent } from './item-shiver/item-shiver.component';
import { ItemDefeatedComponent } from './item-defeated/item-defeated.component';
import { ItemAchingLimbsComponent } from './item-aching-limbs/item-aching-limbs.component';
import { ItemCouchComponent } from './item-couch/item-couch.component';
import { ItemSnuffComponent } from './item-snuff/item-snuff.component';
import { ItemDiarrhoeaComponent } from './item-diarrhoea/item-diarrhoea.component';
import { ItemSoreThroatComponent } from './item-sore-throat/item-sore-throat.component';

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
    ItemSoreThroatComponent
  ],
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: QuestionaireComponent
      },
    ]),
    CommonModule,
    FormsModule
  ]
})
export class QuestionaireModule { }
