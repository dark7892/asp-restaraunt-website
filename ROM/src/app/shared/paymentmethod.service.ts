import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentmethodService {

  constructor(private http: HttpClient) { }

  getPaymentMethods() {
    return this.http.get(environment.apiURL + '/paymentmethods').toPromise();
  }
}
