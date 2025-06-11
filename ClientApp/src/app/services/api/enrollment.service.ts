import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {
  url = 'Enrollment'
  constructor(private api: ApiService) { }
  enrollMultiple(studentData: any) {
    return this.api.post(`${this.url}/multiple-with-plan`, studentData)
  }
}
