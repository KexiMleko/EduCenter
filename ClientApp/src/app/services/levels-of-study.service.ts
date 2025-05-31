import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class LevelsOfStudyService {
  url = 'LevelOfStudy'
  constructor(private api: ApiService) { }
  getAll() {
    return this.api.get(`${this.url}/get-all`)
  }
}
