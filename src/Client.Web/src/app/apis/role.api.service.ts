import { HttpClient } from "@angular/common/http";
import { computed, inject, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { PaginatedResponse, PaginationPayload } from "@shared/components/pagination/pagination.component";
import { CreateRolePayload, Role } from "../types/role/role";
import { OrganizationStore } from "../features/organization/stores/organization.store";

@Injectable({
    providedIn: 'root'
})
export class RoleApiService {
    private http = inject(HttpClient);
    private organizationStore = inject(OrganizationStore);
    organizationId = computed(() => this.organizationStore.data()?.id);

    getRoles(): Observable<Role[]> {
        return this.http.get<any>(`/api/organizations/${this.organizationId()}/roles`).pipe(
            map(response => response.roles)
        );
    }

    getRolesPaginated(payload: PaginationPayload<Role, number>): Observable<PaginatedResponse<Role>> {
        return this.http.post<PaginatedResponse<Role>>(`/api/organizations/${this.organizationId()}/roles/paginated`, payload);
    }

    getRole(roleId: string): Observable<Role> {
        return this.http.get<Role>(`/api/roles/${roleId}`);
    }

    createRole(name: string): Observable<Role> {
        return this.http.post<Role>(`/api/roles`, { name, organizationId: this.organizationId() });
    }

    updateRole(roleId: string, payload: Partial<Role>): Observable<Role> {
        return this.http.put<Role>(`/api/roles/${roleId}`, payload);
    }

    addPermissionsToRole(roleId: number, permissionIds: number[]): Observable<void> {
        return this.http.post<void>(`/api/roles/${roleId}/permissions`, permissionIds);
    }

    removePermissionsFromRole(roleId: number, permissionIds: number[]): Observable<void> {
        return this.http.delete<void>(`/api/roles/${roleId}/permissions`, { body: permissionIds });
    }

    addMembersToRole(roleId: number, memberIds: number[]): Observable<void> {
        return this.http.post<void>(`/api/roles/${roleId}/members`, memberIds);
    }

    removeMembersFromRole(roleId: number, memberIds: number[]): Observable<void> {
        return this.http.delete<void>(`/api/roles/${roleId}/members`, { body: memberIds });
    }

    deleteRole(roleId: number): Observable<void> {
        return this.http.delete<void>(`/api/roles/${roleId}`);
    }
}
