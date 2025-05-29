import { Observable } from "rxjs";
import { PagedRequest } from "../models/wrappers/PagedRequest";

export interface BaseService {
  getPagedData(request: PagedRequest<any>): Observable<any>
}
