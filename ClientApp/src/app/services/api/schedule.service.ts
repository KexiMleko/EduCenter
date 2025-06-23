import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Observable } from 'rxjs';
import { TeacherSchedule } from 'src/app/models/teacher-schedule';

@Injectable({
  providedIn: 'root',
})
export class ScheduleService {
  url = 'Schedule';
  constructor(private api: ApiService) {}
  getCurrentUserSchedule(): Observable<TeacherSchedule> {
    return this.api.get(`${this.url}/get-current`);
  }
}
