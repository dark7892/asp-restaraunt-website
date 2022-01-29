import { Component, OnInit } from '@angular/core';
import { Item } from '../shared/item.model';
import { ItemService } from '../shared/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styles: []
})
export class ItemsComponent implements OnInit {
  products: Item[];
  constructor(private service: ItemService) { }

  ngOnInit() {
    this.getItems();
  }

  getItems() {
    this.service.getItems().then(res => this.products = res as Item[]);
  };


}
