import { MatTableDataSource } from '@angular/material/table';
import { BaseService } from './base-service';
import { catchError, of } from 'rxjs';
import { Directive, inject } from '@angular/core';
import { HotToastService } from '@ngxpert/hot-toast';
import { PagedRequest } from '../models/wrappers/PagedRequest';
import { PagedResult } from '../models/wrappers/PagedResult';
import { PageEvent } from '@angular/material/paginator';
@Directive()
export abstract class BaseTableComponent<TData = any, TFilter = any> {
  dataSource = new MatTableDataSource<TData>();
  totalCount: number;
  startIndex: number;
  limit: number = 5;
  toast = inject(HotToastService);

  constructor(private service: BaseService) { }

  loadTableData(filter: TFilter): void {
    console.log(this.limit)
    this.service.getPagedData(this.createPagedRequest(filter)).pipe(
      this.toast.observe({
        loading: 'Ucitavanje...',
        success: () => 'Uspesno ucitano',
        error: () => 'Greska prilikom ucitavanja'
      }),
      catchError(err => {
        console.error(err)
        return of({ data: [], totalCount: 0 });
      }),
    ).subscribe({
      next: (response: PagedResult<TData>) => {
        console.log(response)
        this.dataSource.data = response.items;
        this.totalCount = response.totalCount;
      },
      error: (err) => {
        console.error(err)
      }
    });
  }
  createPagedRequest(filter: TFilter): PagedRequest<TFilter> {
    return {
      startIndex: this.startIndex,
      limit: this.limit,
      filters: filter
    };
  }
  onPageChange(event: PageEvent, filter: TFilter) {
    this.limit = event.pageSize;
    this.startIndex = event.pageIndex;

    this.loadTableData(filter);
  }
}
