import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class IndividualSessionService {

  url = 'IndividualSession'
  constructor(private api: ApiService) { }
  scheduleSession(session: any) {
    return this.api.post(`${this.url}/create`, session)
  }
}
