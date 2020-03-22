import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { HomeComponent } from './components/home/home.component';


// TODO: missing guards
const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'driver-signup-confirmation', component: ResetPasswordComponent },
  {
    path: 'questionaire',
    loadChildren: () => import('./components/questionaire/questionaire.module').then(m => m.QuestionaireModule)
  },
  {
    path: 'medical-institute',
    loadChildren: () => import('./modules/medical-institute/medical-institute.module').then(m => m.MedicalInstituteModule)
  }
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);
