import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { OrderItem } from 'src/app/shared/order-item.model';
import { ItemService } from 'src/app/shared/item.service';
import { Item } from 'src/app/shared/item.model';
import { NgForm } from '@angular/forms';
import { OrderService } from 'src/app/shared/order.service';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styles: []
})
export class OrderItemsComponent implements OnInit {
  formData: OrderItem;
  itemList: Item[];
  isValid: boolean = true;

  updateTotal() {
    if (this.formData.Quantity <= 0) {
      this.formData.Total = parseFloat((0).toFixed(2));
    }
    else {
      this.formData.Total = parseFloat((this.formData.Price * this.formData.Quantity).toFixed(2));
    }
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data,
    public dialogRef: MatDialogRef<OrderItemsComponent>,
    private itemService: ItemService,
    private orderService: OrderService) { }

  ngOnInit() {
    this.itemService.getItems().then(res => this.itemList = res as Item[]);

    if (this.data.OrderItemIndex == null) {
      this.formData = {
        OrderDetailId: 0,
        OrderId: this.data.OrderId,
        ItemId: 0,
        ItemName: '',
        Price: 0,
        Quantity: 0,
        Total: 0
      }
    }
    else {
      this.formData = Object.assign({}, this.orderService.orderItems[this.data.OrderItemIndex]);
    }
  }

  updateData(ctrl) {
    if (ctrl.selectedIndex == 0) {
      this.formData.Price = 0;
      this.formData.ItemName = '';
      this.formData.Total = 0;
      this.formData.Quantity = 0;
    }
    else {
      this.formData.Price = this.itemList[ctrl.selectedIndex - 1].Price;
      this.formData.ItemName = this.itemList[ctrl.selectedIndex - 1].Name;
      if (this.formData.Quantity > 0) {
        this.formData.Total = this.itemList[ctrl.selectedIndex - 1].Price * this.formData.Quantity;
      }
    }
  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      if(this.data.OrderItemIndex == null) {//PUSH
        this.orderService.orderItems.push(form.value);
        console.log("Order item added: " + this.formData.ItemName);
      }
      else{ //EDIT
        this.orderService.orderItems[this.data.OrderItemIndex] = form.value;
        console.log("Order item edited: " + this.formData.ItemName);
      }
      this.dialogRef.close();
    }

  }

  validateForm(formData: OrderItem) {
    this.isValid = true;
    if (this.formData.ItemId == 0) {
      this.isValid = false;
    }
    else if (this.formData.Quantity <= 0) {
      this.isValid = false;
    }
    return this.isValid;
  }
}
