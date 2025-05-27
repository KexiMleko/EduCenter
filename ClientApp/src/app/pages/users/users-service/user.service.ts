import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private api: ApiService) { }
  addUser(user: any) {
    return this.api.post('user/create', user)
  }
  getAllUsers() {
    return this.api.get('user/get-all')
  }
}
