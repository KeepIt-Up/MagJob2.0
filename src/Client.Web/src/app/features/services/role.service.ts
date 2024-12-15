import { query } from '@angular/animations';
import { inject, Injectable, signal } from '@angular/core';
import { RoleApiService } from '@features/apis/role.api.service';
import { Permission, Role } from '@features/models/role/role';
import { PaginatedResponse, PaginationOptions } from '@shared/components/pagination/pagination.component';
import { NotificationService } from '@shared/services/notification.service';
import { StateService } from '@shared/services/state.service';
import { catchError, tap, throwError } from 'rxjs';
import { PermissionService } from './permission.service';
import { ListStateService } from '@shared/services/list-state.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private readonly apiService = inject(RoleApiService);
  private readonly notificationService = inject(NotificationService);
  private readonly permissionService = inject(PermissionService);
  private readonly roleStateService = new ListStateService<Role, { endOfData: boolean }>();

  permissions$ = this.permissionService.permissionState$;
  roles$ = this.roleStateService.state$;

  paginationOptions$ = signal<PaginationOptions<Role>>({
    pageNumber: 1,
    pageSize: 10,
    sortField: "id",
    ascending: true
  });


  selectedRole$ = signal<Role | undefined>(undefined);
  selectRole(role: Role) {
    this.selectedRole$.set(role);
  }

  getRoles(organizationId: string) {
    const query = { organizationId };
    return this.apiService.getAll(query, this.paginationOptions$()).pipe(
      tap((response: PaginatedResponse<Role>) => {
        response.items.forEach((role) => this.roleStateService.add(role));
        this.roleStateService.setMetadata({ endOfData: !response.hasNextPage });
      }),
      catchError((error) => {
        this.roleStateService.setError(error);
        throw error;
      })
    );
  }

  createRole(organizationId: string, roleName: string) {
    return this.apiService.create({ organizationId: organizationId, roleName: roleName }).pipe(
      tap((createdRole) => {
        this.roleStateService.setData([...this.roles$().data ?? [], createdRole]);
      }),
      catchError((error) => {
        this.roleStateService.setError(error);
        this.notificationService.show('Failed to create role', 'error');
        return throwError(() => error);
      })
    );
  }

  updateRole(roleId: string, role: Partial<Role>) {
    return this.apiService.update(roleId, role).pipe(
      tap((updatedRole) => {
        this.roleStateService.update(updatedRole);
        this.notificationService.show('Role updated successfully', 'success');
      }),
      catchError((error) => {
        this.notificationService.show('Failed to update role', 'error');
        return throwError(() => error);
      })
    );
  }

  deleteRole() {
    return this.apiService.delete(this.selectedRole$()!.id).pipe(
      tap(() => {
        this.roleStateService.remove(this.selectedRole$()!);
        this.selectedRole$.set(undefined);
        this.notificationService.show('Role deleted successfully', 'success');
      }),
      catchError((error) => {
        this.notificationService.show('Failed to delete role', 'error');
        return throwError(() => error);
      })
    );
  }

  updateRolePermissions(permissions: { permission: Permission, value: boolean }[]) {
    if (!this.selectedRole$()) return;

    const currentPermissions = this.selectedRole$()!.permissions;
    const addPermissions: string[] = [];
    const removePermissions: string[] = [];

    // Check which permissions need to be added/removed
    permissions.forEach(({ permission, value }) => {
      const hasPermission = currentPermissions.some(p => p.id === permission.id);

      if (value && !hasPermission) {
        // Permission was turned on but not currently assigned
        addPermissions.push(permission.id);
      } else if (!value && hasPermission) {
        // Permission was turned off but currently assigned
        removePermissions.push(permission.id);
      }
    });

    // Create array of promises for API calls
    const updatePromises: Promise<any>[] = [];

    if (addPermissions.length) {
      updatePromises.push(
        this.addPermissionsToRole(
          this.selectedRole$()!.id,
          addPermissions
        ).toPromise()
      );
    }

    if (removePermissions.length) {
      updatePromises.push(
        this.removePermissionsFromRole(
          this.selectedRole$()!.id,
          removePermissions
        ).toPromise()
      );
    }

    // Execute all updates
    if (updatePromises.length) {
      Promise.all(updatePromises)
        .then(() => {
          this.notificationService.show('Permissions updated successfully', 'success');
          this.selectedRole$()!.permissions = permissions.filter(p => p.value).map(p => p.permission);
        })
        .catch(() => {
          this.notificationService.show('Failed to update permissions', 'error');
        });
    }
  }

  addPermissionsToRole(roleId: string, permissionIds: string[]) {
    return this.apiService.addPermissionsToRole(roleId, permissionIds);
  }

  removePermissionsFromRole(roleId: string, permissionIds: string[]) {
    return this.apiService.removePermissionsFromRole(roleId, permissionIds);
  }

  addMembersToRole(roleId: string, memberIds: string[]) {
    return this.apiService.addMembersToRole(roleId, memberIds).pipe(
      tap(() => {
        this.notificationService.show('Members assigned successfully', 'success');
      }),
      catchError((error) => {
        this.notificationService.show('Failed to assign members', 'error');
        return throwError(() => error);
      })
    );
  }

  removeMembersFromRole(roleId: string, memberIds: string[]) {
    return this.apiService.removeMembersFromRole(roleId, memberIds).pipe(
      tap(() => {
        this.notificationService.show('Members removed successfully', 'success');
      }),
      catchError((error) => {
        this.notificationService.show('Failed to remove members', 'error');
        return throwError(() => error);
      })
    );
  }
}
