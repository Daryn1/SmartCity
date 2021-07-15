import { Component, OnInit, Input } from "@angular/core";
import { Product } from "../../models/product";
import { DataService } from "../../services/data.service";

@Component({
    selector: "product-create",
    templateUrl: "./product-create.component.html",
    styleUrls: ["./product-create.component.css"],
})
export class ProductCreateComponent implements OnInit {
    @Input() product = {
        id: "0",
        name: "",
        description: "",
        imageUrl: "",
        price: 0,
        unitsInStock: 0,
        creationDate: new Date(),
        modificationDate: new Date(),
    };

    constructor(private data: DataService) {}

    ngOnInit() {}

    addEmployee() {
        this.data.createProduct(this.product).subscribe();
    }
}
