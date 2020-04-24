import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WaitListService {

  constructor(private http: HttpClient) { }

  public postAppointment(payload: any) {
    return this.http.post('/api/Apppointments/', payload).toPromise();
  }

  public getAppointment() {
    return this.http.get('/api/Apppointments/').toPromise();
  }

  public updateAppointment(payload: any) {
    return this.http.put('/api/Apppointments/' + payload.Id.toString(), payload).toPromise();
  }
}
