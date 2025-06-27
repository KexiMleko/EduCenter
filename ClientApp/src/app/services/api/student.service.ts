import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/base/base-service';
import { StudentBrief } from 'src/app/models/student-brief';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root',
})
export class StudentService implements BaseService {
  url = 'student';
  constructor(private api: ApiService) { }
  getPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post('student/get-paged', request);
  }
  addStudent(student: any) {
    return this.api.post('student/create', student);
  }
  getStudentByGroup(groupId: number) {
    return this.api.get(`${this.url}/get-by-group/${groupId}`);
  }

  getBriefPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post('student/get-brief-paged', request);
  }
  getStudentBriefById(id: number): Observable<StudentBrief> {
    return this.api.get(`${this.url}/get-brief-by-id/${id}`);
  }
}
