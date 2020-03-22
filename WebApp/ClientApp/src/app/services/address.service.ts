import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { AbstractHttpService } from './abstract-http.service';
import { Address } from '../models/address.model';

@Injectable({
  providedIn: 'root',
})
export class AddressService extends AbstractHttpService {

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(baseUrl + 'api/address');
  }

  suggestAddresses(address: Address): Observable<Address[]> {
    return this.http.post<Address[]>(`${this.baseUrl}/suggestaddresses`, address, this.httpOptions);
  }
}
