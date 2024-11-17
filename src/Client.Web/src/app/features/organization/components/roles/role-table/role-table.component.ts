import { Component, inject, Input, OnInit } from '@angular/core';
import { ColumnDefinition } from '../../../../../shared/components/table/table.component';
import { RoleApiService } from '../../../../../apis/role.api.service';
import { TableWithPaginationComponent } from '../../../../../shared/components/table-with-pagination/table-with-pagination.component';
import { SearchInputComponent } from '../../../../../shared/components/search-input/search-input.component';
import { PaginationPayload } from '../../../../../shared/components/pagination/pagination.component';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '@shared/services/notification.service';
import { RoleActionsComponent } from './role-actions.component';
import { Role } from '../../../../../types/role/role';

@Component({
  selector: 'app-roles-table',
  standalone: true,
  imports: [TableWithPaginationComponent, SearchInputComponent, RouterLink],
  templateUrl: './role-table.component.html',
  styleUrl: './role-table.component.css'
})
export class RolesTableComponent implements OnInit {
  roleService = inject(RoleApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  @Input() organizationId!: string;
  payload!: PaginationPayload<Role, number>;
  notificationService = inject(NotificationService);

  ngOnInit(): void {
    this.route.parent?.params.subscribe(params => {
      this.organizationId = params['organizationId'];
      this.payload = {
        payload: Number(this.organizationId),
        pageNumber: 1,
        pageSize: 10,
        sortField: "id",
        ascending: true
      };
    });
  }

  columnsConfig: ColumnDefinition<Role>[] = [
    {
      title: "ID",
      modelProp: "id",
    },
    {
      title: "Name",
      modelProp: 'name',
      isSortable: true
    },
    {
      title: "Description",
      modelProp: 'description',
      isSortable: true
    },
    {
      title: "Members Count",
      modelProp: 'members',
      computeValue: (row) => row.members.length.toString()
    },
    {
      title: "Actions",
      modelProp: 'id',
      component: {
        type: RoleActionsComponent,
        inputs: (row) => ({
          roleId: row.id,
          onEditCallback: () => this.navigateToRoleManagement(row.id),
        }),
      }
    }
  ];

  navigateToRoleManagement(roleId: string) {
    this.router.navigate(['/organization', this.organizationId, 'roles', roleId]);
  }

  deleteRole(roleId: string) {
    // Implement role deletion logic
  }
} 