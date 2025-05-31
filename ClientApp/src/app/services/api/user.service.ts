import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/base/base-service';
import { PagedRequest } from 'src/app/models/wrappers/PagedRequest';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService implements BaseService {

  constructor(private api: ApiService) { }
  getPagedData(request: PagedRequest<any>): Observable<any> {
    return this.api.post('user/get-paged', request)
  }
  addUser(user: any) {
    return this.api.post('user/create', user)
  }
}
