import { Injectable } from '@angular/core';

@Injectable()
export class DataService {
  constructor() {}

  public products = [
    {
      title: '1 product',
      price: 20,
    },
    {
      title: '2 product',
      price: 30,
    },
    {
      title: '3 product',
      price: 40,
    },
  ];
}
