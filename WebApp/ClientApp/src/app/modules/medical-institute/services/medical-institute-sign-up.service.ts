import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractHttpService } from 'src/app/services/abstract-http.service';
import { ResetPassword } from 'src/app/models/reset-password.model';
import { MedicalInstituteSignUp } from '../models/medical-institute-sign-up.model';

@Injectable({ providedIn: 'root' })
export class MedicalInstituteSignUpService extends AbstractHttpService {

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(baseUrl + 'api/medicalinstituteaccount');
  }

  createUser(model: MedicalInstituteSignUp) {
      return this.http.post<any>(`${this.baseUrl}`, model, this.httpOptions);
  }

  confirmEmail(userId: string, token: string) {
    return this.http.get<any>(`${this.baseUrl}/confirmemail?userId=${userId}&token=${token}`);
  }

  resetPassword(model: ResetPassword) {
    return this.http.post<any>(`${this.baseUrl}/resetpassword`, model, this.httpOptions);
  }
}
