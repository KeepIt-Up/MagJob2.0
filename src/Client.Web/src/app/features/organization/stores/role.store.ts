import { computed, inject, Injectable, Signal, signal } from '@angular/core';
import { Role } from '../../../types/role/role';
import { RoleApiService } from '../../../apis/role.api.service';
import { BaseStore } from '../../../shared/stores/base.store';
import { OrganizationStore } from './organization.store';

@Injectable({
    providedIn: 'root'
})
export class RoleStore extends BaseStore<Role[]> {
    private selectedRole = signal<Role>({
        id: '',
        name: '',
        organizationId: '',
        permissions: [],
        members: []
    });
    public selectedRole$ = this.selectedRole.asReadonly();
    constructor(private roleApiService: RoleApiService) {
        super();
    }
    loadRoles(): void {
        this.setLoading();

        this.roleApiService.getRoles().subscribe({
            next: (roles) => {
                if (roles.length == 0) {
                    this.setError('No roles found');
                } else {
                    this.selectRole(roles[0]);
                    this.setData(roles);
                }
            },
            error: (error) => this.setError(error)
        });
    }

    addRole(role: Role): void {
        const currentRoles = this.data() || [];
        this.setData([...currentRoles, role]);
        this.selectRole(role);
    }

    updateRole(updatedRole: Role): void {
        const currentRoles = this.data() || [];
        const index = currentRoles.findIndex(r => r.id === updatedRole.id);
        if (index !== -1) {
            const newRoles = [...currentRoles];
            newRoles[index] = updatedRole;
            this.setData(newRoles);
        }
    }

    removeRole(roleId: string): void {
        const currentRoles = this.data() || [];
        const newRoles = currentRoles.filter(r => r.id !== roleId);
        this.setData(newRoles);
        this.selectRole(newRoles[0]);
    }

    selectRole(role: Role): void {
        this.selectedRole.set(role);
    }
} 