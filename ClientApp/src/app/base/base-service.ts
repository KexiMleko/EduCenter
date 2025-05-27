import { Observable } from "rxjs";

export interface BaseService {
  getPagedData(): Observable<any>
}
