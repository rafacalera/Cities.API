import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Web';
  private readonly API = 'http://localhost:5222/api/v1/Cities';
  cities: Array<any> = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.list();
  }

  list(): void {
    this.http.get(this.API).subscribe((data) => {
      this.cities = data as Array<any>;
    });
  }
}
