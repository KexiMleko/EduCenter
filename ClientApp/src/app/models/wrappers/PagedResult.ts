export interface PagedResult<T> {
  startIndex: number;
  limit: number;
  totalCount: number;
  items: T[];
}
