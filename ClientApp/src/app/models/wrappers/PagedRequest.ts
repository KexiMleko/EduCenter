export interface PagedRequest<T> {
  startIndex: number;
  limit: number;
  filters?: T;
}
