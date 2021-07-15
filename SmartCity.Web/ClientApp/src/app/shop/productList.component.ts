import { Component, OnInit } from "@angular/core";
import { Product } from "../models/product";
import { DataService } from "../services/data.service";

@Component({
    selector: "product-list",

    templateUrl: "productList.component.html",

    styleUrls: [],
})
export class ProductList implements OnInit {
    public products: Product[] = [];
    
    constructor(private data: DataService) {
    }

    ngOnInit(): void {
        this.data.getProducts().subscribe((products) => {
            this.products = products;
        });
    }
}
