import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let httpMock: HttpTestingController;
  let fixture: any;
  let component: any;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      declarations: [AppComponent],
    }).compileComponents();

    httpMock = TestBed.inject(HttpTestingController);
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it(`should have as title 'Web'`, () => {
    expect(component.title).toEqual('Web');
  });

  it('should render caption of table', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h2')?.textContent).toContain(
      'Table of Cities'
    );
  });

  it('should be City and State at head of table', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;

    const thElements = compiled.querySelectorAll('th');
    expect(thElements[0]?.textContent).toContain('City');
    expect(thElements[1]?.textContent).toContain('State');
  });

  it('should make a GET request to fetch cities', () => {
    fixture.detectChanges();
    const mockCities = [
      { name: 'City1', state: 'State1' },
      { name: 'City2', state: 'State2' },
    ];

    const req = httpMock.expectOne('http://localhost:5222/Cities');
    req.flush(mockCities);

    component.list();

    expect(req.request.method).toBe('GET');
    expect(component.cities).toEqual(mockCities);
  });
});
