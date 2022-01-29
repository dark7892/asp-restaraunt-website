import { Injectable } from '@angular/core';
import { Order } from './order.model';
import { OrderItem } from './order-item.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { OrderView } from './orderView.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  formData: Order;
  orderItems: OrderItem[];

  constructor(private http: HttpClient) { }

  saveOrUpdateOrder() {
    var body = {
      ...this.formData,
      OrderItems: this.orderItems
    }
    return this.http.post(environment.apiURL + '/orders', body);
  }

  getOrdersList(): Observable<OrderView[]> {
    return this.http.get<OrderView[]>(environment.apiURL + '/orders');
  }

  getOrderById(id: number):any {
    return this.http.get(environment.apiURL + '/orders/' + id).toPromise();
  }

  deleteOrder(id: number) {
    return this.http.delete(environment.apiURL + '/orders/'+ id).toPromise();
  }


}

