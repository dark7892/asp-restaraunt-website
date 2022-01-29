import { Component, OnInit, Inject } from '@angular/core';
import { OrderService } from '../shared/order.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderView } from '../shared/orderView.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styles: []
})
export class OrdersComponent implements OnInit {
  orderList: OrderView[]
  filteredOrders: OrderView[];
  _listFilter = '';

  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredOrders = this.listFilter ? this.performFilter(this.listFilter) : this.orderList;
  }

  performFilter(filterBy: string): OrderView[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.orderList.filter((order: OrderView) => order.Customer.toLocaleLowerCase().indexOf(filterBy) !== -1 || order.OrderNo.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  constructor(
    private service: OrderService,
    private router: Router,
    private toastr: ToastrService) {
    this.filteredOrders = this.orderList;
  }

  ngOnInit() {
    this.refreshList();
  }

  refreshList() {
    this.service.getOrdersList().subscribe({
      next: orderList => {
        this.orderList = orderList;
        this.filteredOrders = this.orderList;
      }
    });
  }

  openForEdit(orderId: number) {
    var route = '/order/edit/' + orderId;
    console.log('Trying to navigate to: ' + route);
    this.router.navigate([route]);
    console.log("navigated to " + route);
  }

  redirectToMain() {
    this.router.navigate(['/order']);
  }

  deleteOrder(orderId: number, orderNo: string) {
    console.log("Deleting the Order with Id: " + orderId);
    this.service.deleteOrder(orderId).then(res => {
      this.refreshList();
      this.toastr.warning(message, "Order deleted!");
    });

    var message: string = "Order no " + orderNo + " has been deleted successfully!";
  }
}
