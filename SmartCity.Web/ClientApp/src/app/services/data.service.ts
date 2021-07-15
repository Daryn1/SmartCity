import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Product } from "../models/product";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";

@Injectable()
export class DataService {
    apiURL = "https://localhost:44342/api";

    constructor(private http: HttpClient) {}

    // Http Options
    httpOptions = {
        headers: new HttpHeaders({
            "Content-Type": "application/json",
        }),
    };

    // HttpClient API get() method => Fetch product list
    getProducts(): Observable<Product[]> {
        return this.http
            .get<Product[]>(this.apiURL + "/products")
            .pipe(retry(1), catchError(this.handleError));
    }

    // HttpClient API get() method => Fetch product
    getProduct(id: number): Observable<Product> {
        return this.http
            .get<Product>(this.apiURL + "/products/" + id)
            .pipe(retry(1), catchError(this.handleError));
    }

    // HttpClient API post() method => Create product
    createProduct(product : Product): Observable<Product> {
        return this.http
            .post<Product>(
                this.apiURL + "/products",
                JSON.stringify(product),
                this.httpOptions
            )
            .pipe(retry(1), catchError(this.handleError));
    }

    // HttpClient API put() method => Update product
    updateProduct(id : number, product : Product): Observable<Product> {
        return this.http
            .put<Product>(
                this.apiURL + "/products/" + id,
                JSON.stringify(product),
                this.httpOptions
            )
            .pipe(retry(1), catchError(this.handleError));
    }

    // HttpClient API delete() method => Delete product
    deleteProduct(id: number) {
        return this.http
            .delete<Product>(this.apiURL + "/products/" + id, this.httpOptions)
            .pipe(retry(1), catchError(this.handleError));
    }

    handleError(err: any) {
        let message = "";
        if (err.error instanceof ErrorEvent) {
            message = err.error.message;
        } else {
            message = `Error Code: ${err.status}\nMessage: ${err.message}`;
        }
        console.log(message);
        return throwError(message);
    }
}
