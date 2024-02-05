import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Web';
  private readonly API = 'http://localhost:5222/Cities';
  cityList: any;

  constructor(private http: HttpClient) {}

  list() {
    this.http.get(this.API).subscribe((data) => (this.cityList = data));
  }
}
