import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/base/base-service';
import { ApiService } from '../api.service';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroupService implements BaseService {
  url = 'group'
  constructor(private api: ApiService) { }
  getPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post(`${this.url}/get-paged`, request)
  }
  addgroup(group: any) {
    return this.api.post(`${this.url}/create`, group)
  }
}
