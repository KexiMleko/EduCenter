import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { LevelOfStudyAddDto } from '../models/add-dtos/level-of-study-add-dto';

@Injectable({
  providedIn: 'root',
})
export class LevelsOfStudyService {
  url = 'LevelOfStudy';
  constructor(private api: ApiService) { }
  addLevel(level: LevelOfStudyAddDto) {
    return this.api.post(`${this.url}/create`, level);
  }
  getAll() {
    return this.api.get(`${this.url}/get-all`);
  }
}
