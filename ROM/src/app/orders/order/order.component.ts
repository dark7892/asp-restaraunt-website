import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/shared/order.service';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { OrderItemsComponent } from '../order-items/order-items.component';
import { Paymentmethod } from 'src/app/shared/paymentmethod.model';
import { PaymentmethodService } from 'src/app/shared/paymentmethod.service';
import { CustomerService } from 'src/app/shared/customer.service';
import { Customer } from 'src/app/shared/customer.model';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styles: []
})
export class OrderComponent implements OnInit {
  paymentMethodsList: Paymentmethod[];
  customersList: Customer[];
  isValid: boolean = true;

  constructor(
    private service: OrderService,
    private dialog: MatDialog,
    private paymentService: PaymentmethodService,
    private customerService: CustomerService,
    private toastr: ToastrService,
    private router: Router,
    private currentRoute: ActivatedRoute) { }


  ngOnInit() {
    let orderId = this.currentRoute.snapshot.paramMap.get('id'); //retrieves ID value from route.
    if (orderId == null)
      this.resetForm();
    else {
      this.service.getOrderById(parseInt(orderId)).then(res => {
        this.service.formData = res.Order;
        this.service.orderItems = res.OrderItems;
      });
    }

    this.paymentService.getPaymentMethods()
      .then(res => this.paymentMethodsList = res as Paymentmethod[]);
    this.customerService.getCustomers()
      .then(res =>
        this.customersList = res as Customer[]);
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.service.formData = {
      OrderId: 0,
      OrderNo: Math.floor(100000 + Math.random() * 900000).toString(),
      CustomerId: 0,
      PaymentMethod: '',
      Total: 0,
      DeletedOrderItemsIds: []
    };

    this.service.orderItems = [];
  }

  validateForm() {
    this.isValid = true;
    if (this.service.formData.CustomerId == 0) {
      this.isValid = false;
    }
    else if (this.service.orderItems.length == 0) {
      this.isValid = false;
    }
    else if (this.service.formData.PaymentMethod == '') {
      this.isValid = false;
    }

    return this.isValid;
  }

  getValidationErrors() {
    var errorsArray: string[];

    if (this.service.formData.CustomerId == 0) {
      errorsArray.push("Please select customer");
    }
    else if (this.service.orderItems.length == 0) {
      errorsArray.push("Please select items to order");
    }
    else if (this.service.formData.PaymentMethod == '') {
      errorsArray.push("Please select payment method");
    }

    return errorsArray;
  }

  addOrEditOrderItem(OrderItemIndex, OrderId) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = false; //prevents dialog from closing if one clicks outside the dialog window
    dialogConfig.width = "50%";
    //Pasing the OrderItemIndex and OrderId
    dialogConfig.data = { OrderItemIndex, OrderId };
    this.dialog.open(OrderItemsComponent, dialogConfig).afterClosed().subscribe(res => {
      this.updateGrandTotal();
    });
  }

  onSubmit(form: NgForm) {
    if (this.validateForm()) {
      console.log("Submitting the form...");
      try {
        this.postOrder(form);
        console.log("Form has been submitted successfully!")
      }
      catch (e) {
        throw new Error('An error occured: ' + e);
      }
    }
    else {
      this.toastr.error("Form input data is invalid. Please check the form.", "Error!");
    }
  }

  postOrder(form: NgForm) {
    this.service.saveOrUpdateOrder().subscribe(res => {
      this.resetForm();
      this.toastr.success("Order has been added! Order no: " + this.service.formData.OrderNo, "Success!");
      this.router.navigate(['/orders']);
    })
  }

  deleteItem(orderDetailId: number, index: number) {
    this.service.formData.DeletedOrderItemsIds = [];

    if (orderDetailId != 0) {
      this.service.formData.DeletedOrderItemsIds.push(orderDetailId);
    }
    this.service.orderItems.splice(index, 1);
    this.updateGrandTotal();
    this.toastr.warning("Item has been deleted from the list!", "Item removed")
  }

  updateGrandTotal() {
    this.service.formData.Total = parseFloat((this.service.orderItems.reduce((prev, curr) => {
      return prev + curr.Total;
    }, 0)).toFixed(2));
  }

  openDialog() {
    console.log("success");
  }
}
