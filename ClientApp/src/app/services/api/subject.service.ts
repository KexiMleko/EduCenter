import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  url = 'Subject'
  constructor(private api: ApiService) { }
  getAll() {
    return this.api.get(`${this.url}/get-all`)
  }
}
