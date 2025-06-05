import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/base/base-service';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService implements BaseService {
  url = 'user'
  constructor(private api: ApiService) { }
  getPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post(`${this.url}/get-paged`, request)
  }
  addUser(user: any) {
    return this.api.post(`${this.url}/create`, user)
  }
  getByRole(roleId: number) {
    return this.api.get(`${this.url}/get-by-role/${roleId}`)
  }
}
