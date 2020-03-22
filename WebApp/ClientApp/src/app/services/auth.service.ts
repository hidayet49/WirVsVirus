import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { Primitive } from 'src/app/models/primitive.model';
import { Login } from 'src/app/models/login.model';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  private accountTypeSubject = new Subject<string>();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.baseUrl = baseUrl + 'api/auth';
    const accountType = localStorage.getItem('account_type');
    if (accountType) {
      this.accountTypeSubject.next(accountType);
    }
  }

  login(model: Login) {
    return this.http.post<any>(`${this.baseUrl}/login`, JSON.stringify(model), httpOptions)
      .pipe(map((res) => {
        this.logout();
        if (model.rememberMe) {
          localStorage.setItem('auth_token', res.auth_token);
          localStorage.setItem('account_type', res.account_type);
        } else {
          sessionStorage.setItem('auth_token', res.auth_token);
          // sessionStorage.setItem('account_type', res.account_type);
        }
        this.accountTypeSubject.next(res.account_type);
        return true;
      }));
  }

  logout() {
    localStorage.removeItem('auth_token');
    // localStorage.removeItem('account_type');
    sessionStorage.removeItem('auth_token');
    // sessionStorage.removeItem('account_type');
    this.accountTypeSubject.next('Anonym');
  }

  sendConfirmationMail(userName: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/sendemailconfirmationmail`, new Primitive(userName), httpOptions);
  }

  sendPasswordResetMail(userName: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/sendpasswordresetmail`, new Primitive(userName), httpOptions);
  }

  getAuthToken(): string {
    const authToken = sessionStorage.getItem('auth_token');
    return authToken ? authToken : localStorage.getItem('auth_token');
  }

  getAccountType(): string {
    let accountType = sessionStorage.getItem('account_type');
    accountType = accountType ? accountType : localStorage.getItem('account_type');
    return accountType ? accountType : 'Anonym';
  }

  observeAccountType(): Observable<string> {
    return this.accountTypeSubject.asObservable();
  }
}
