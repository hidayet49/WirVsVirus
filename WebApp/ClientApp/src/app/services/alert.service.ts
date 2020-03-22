import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertSnackBarComponent } from '../modules/custom-angular-material/components/alert-snackbar/alert-snackbar.component';

@Injectable({ providedIn: 'root' })
export class AlertService {
  private subject = new Subject<any>();
  private keepAfterNavigationChange = false;
  constructor(private router: Router,
    private snackBar: MatSnackBar) {
    // clear alert message on route change
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterNavigationChange) {
          // only keep for a single location change
          this.keepAfterNavigationChange = false;
        }
      }
    });
  }
  success(message: string, keepAfterNavigationChange = false) {
    this.keepAfterNavigationChange = keepAfterNavigationChange;
    this.snackBar.openFromComponent(AlertSnackBarComponent, {
      duration: 4000,
      data: {
        title: 'Erledigt!',
        message: message,
        type: 'success'
      }
    });
  }
  error(err: any) {
    if (typeof (err) === 'string') {
      this.showError(err);
    } else {
      const contentTypeHeaders = (err.headers as HttpHeaders).get('Content-Type');
      if (contentTypeHeaders && contentTypeHeaders.includes('text/html')) {
        this.showError(err.statusText);
      }
      else if (typeof (err.error) === 'string') {
        this.showError(err.error);
      } else if (err.error) {
        for (let key in err.error) {
          this.showError(err.error[key]);
        }
      } else {
        for (let key in err) {
          this.showError(err[key]);
        }
      }
    }
  }

  private showError(message: string) {
    this.snackBar.openFromComponent(AlertSnackBarComponent, {
      duration: 4000,
      data: {
        title: 'Fehlermeldung!',
        message: message,
        type: 'error'
      }
    });
  }

  getMessage(): Observable<any> {
    return this.subject.asObservable();
  }
}