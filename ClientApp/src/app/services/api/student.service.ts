import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/base/base-service';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class StudentService implements BaseService {

  constructor(private api: ApiService) { }
  getPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post('student/get-paged', request)
  }
  addStudent(student: any) {
    return this.api.post('student/create', student)
  }
}
