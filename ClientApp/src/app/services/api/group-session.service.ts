import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { GroupSessionAddDto } from 'src/app/models/add-dtos/group-session-add-dto';

@Injectable({
  providedIn: 'root'
})
export class GroupSessionService {

  url = 'GroupSession'
  constructor(private api: ApiService) { }
  scheduleGroupSession(session: GroupSessionAddDto) {
    return this.api.post(`${this.url}/create`, session)
  }
}
