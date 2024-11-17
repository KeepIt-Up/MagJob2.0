import { Component, inject, OnInit } from '@angular/core';
import { TableWithPaginationComponent } from '../../../../../shared/components/table-with-pagination/table-with-pagination.component';
import { ColumnDefinition } from '../../../../../shared/components/table/table.component';
import { InvitationService } from '../../../../../apis/invitation.api.service';
import { PaginationPayload } from '../../../../../shared/components/pagination/pagination.component';
import { SearchInputComponent } from '../../../../../shared/components/search-input/search-input.component';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Invitation, InvitationStatus } from '../../../../../types/invitation/invitation';
import { DatePipe } from '@angular/common';
import { TagComponent } from '../../../../../shared/components/tag/tag.component';

function getStatusVariant(status: number): 'yellow' | 'green' | 'red' | 'gray' {
  switch (status) {
    case 0: return 'yellow';
    case 1: return 'green';
    case 2: return 'red';
    case 3: return 'gray';
    default: return 'gray';
  }
}

@Component({
  selector: 'app-invitation-table',
  standalone: true,
  imports: [TableWithPaginationComponent, SearchInputComponent, RouterLink],
  providers: [DatePipe],
  templateUrl: './invitation-table.component.html',
  styleUrl: './invitation-table.component.css'
})
export class InvitationTableComponent implements OnInit {
  invitationService = inject(InvitationService);
  private route = inject(ActivatedRoute);
  organizationId!: string;
  payload!: PaginationPayload<Invitation, string>;

  ngOnInit(): void {
    this.route.parent?.params.subscribe(params => {
      this.organizationId = params['organizationId'];
      this.payload = {
        payload: this.organizationId,
        pageNumber: 1,
        pageSize: 10,
        sortField: "id",
        ascending: true
      };
    });
  }



  columnsConfig: ColumnDefinition<Invitation>[] = [
    {
      title: "ID",
      modelProp: "id",
    },
    {
      title: "Name",
      modelProp: 'userName'
    },
    {
      title: "Date of creation",
      modelProp: 'createdAt',
      pipe: DatePipe,
      pipeArgs: ['dd/MM/yyyy HH:mm:ss'],
      isSortable: true,
    },
    {
      title: "Status",
      modelProp: 'status',
      component: {
        type: TagComponent,
        inputs: (invitation: Invitation) => ({
          variant: getStatusVariant(invitation.status),
          text: InvitationStatus[invitation.status]
        })
      },
      isSortable: true,
    }
  ];
}
