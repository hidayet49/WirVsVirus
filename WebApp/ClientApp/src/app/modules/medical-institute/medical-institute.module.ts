import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MedicalInstituteSignUpComponent } from './components/medical-institute-sign-up/medical-institute-sign-up.component';
import { MedicalInstituteRoutingModule } from './medical-institute-routing.module';
import { CustomAngularMaterialModule } from '../custom-angular-material/custom-angular-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MedicalInstituteComponent } from './components/medical-institute/medical-institute.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MedicalInstituteRoutingModule,
    CustomAngularMaterialModule,
  ],
  declarations: [
    MedicalInstituteComponent,
    MedicalInstituteSignUpComponent]
})

export class MedicalInstituteModule {
  static forRoot(): ModuleWithProviders<MedicalInstituteModule> {
    return {
      ngModule: MedicalInstituteModule,
      providers: [
      ]
    };
  }
}