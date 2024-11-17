import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ContainerComponent } from '../container/container.component';
import { ColumnDefinition, SortBy, TableComponent } from '../table/table.component';
import { PaginatedResponse, PaginationComponent, PaginationPayload } from "../pagination/pagination.component";
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table-with-pagination',
  standalone: true,
  imports: [ContainerComponent, TableComponent, PaginationComponent],
  templateUrl: './table-with-pagination.component.html',
  styleUrl: './table-with-pagination.component.css'
})
export class TableWithPaginationComponent<T extends { id: string }, K = undefined> implements OnInit, OnChanges {
  @Input({ required: true }) columnsConfig!: ColumnDefinition<T>[];
  @Input({ required: true }) request!: (payload: PaginationPayload<T, K>) => Observable<PaginatedResponse<T>>;
  @Input() payload: PaginationPayload<T, K> = {
    pageNumber: 1,
    pageSize: 10,
    sortField: "id",
    ascending: true
  };

  private http = inject(HttpClient);
  source$?: Observable<PaginatedResponse<T>>;
  state: any;

  sortBy: SortBy<T> = {
    modelProp: "id",
    ascending: true
  };

  ngOnInit(): void {
    this.update();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.update();
  }

  private update() {
    this.source$ = this.request(this.payload);
  }

  onPageChange(pageNumber: number) {
    this.payload.pageNumber = pageNumber;
    this.update();
  }

  onSortByChange(sortBy: SortBy<T>) {
    this.payload.sortField = sortBy.modelProp;
    this.payload.ascending = sortBy.ascending;
    this.update();
  }

  onPageSizeChange(pageSize: number) {
    this.payload.pageSize = pageSize
    this.update();
  }
}
