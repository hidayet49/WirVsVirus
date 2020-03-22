import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';

import { MedicalInstituteSignUpComponent } from './components/medical-institute-sign-up/medical-institute-sign-up.component';
import { MedicalInstituteComponent } from './components/hospital/medical-institute/medical-institute.component';

const routes: Routes = [
  {
    path: '',
    component: MedicalInstituteComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'medical-institute-sign-up'
      },
      {
        path: 'medical-institute-sign-up',
        component: MedicalInstituteSignUpComponent,
      }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicalInstituteRoutingModule { }
