import { Component, effect, inject, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ScrollControlService } from '../../../../../core/services/scroll-control.service';
import { RoleStore } from '../../../stores/role.store';
import { RoleApiService } from '../../../../../apis/role.api.service';
import { PermissionApiService } from '../../../../../apis/permission.api.service';
import { NotificationService } from '@shared/services/notification.service';
import { MemberService } from '../../../../../apis/member.api.service';
import { Permission, Role } from '../../../../../types/role/role';
import { Member } from '../../../../../types/member/member';
import { RolesListComponent } from '../roles-list/roles-list.component';
import { MemberSearchModalComponent } from '../../members/member-search-modal/member-search-modal.component';
import { TabsComponent } from '@shared/components/tabs/tabs.component';
import { SpinnerComponent } from '@shared/components/spinner/spinner.component';

interface Tab {
  id: string;
  label: string;
}

@Component({
  selector: 'app-roles-management',
  standalone: true,
  imports: [FormsModule, RolesListComponent, MemberSearchModalComponent, TabsComponent, SpinnerComponent],
  templateUrl: './roles-management.component.html',
})
export class RolesManagementComponent implements OnInit, OnChanges, OnDestroy {
  private scrollControlService = inject(ScrollControlService);
  roleStore = inject(RoleStore);
  private roleApiService = inject(RoleApiService);
  private permissionApiService = inject(PermissionApiService);
  private notificationService = inject(NotificationService);
  private memberService = inject(MemberService);

  tabs: Tab[] = [
    { id: 'appearance', label: 'Appearance' },
    { id: 'permissions', label: 'Permissions' },
    { id: 'assignments', label: 'Assignments' }
  ];
  // do wyciągania do komponentu
  activeTab = 'appearance';
  // do wyciągania do komponentu
  loading = this.roleStore.loading;
  error = this.roleStore.error;

  role!: Role;



  permissionsAll: Permission[] = [];
  permissions: { permission: Permission, value: boolean }[] = [];

  memberSearchQuery: string = '';
  members: Member[] = [];
  filteredMembers: Member[] = [];
  showMemberSearchModal = false;

  constructor() {
    effect(() => {
      this.role = this.roleStore.selectedRole$();
      this.permissions = this.permissionsAll.map(permission => {
        if (this.role.permissions.find(p => p.id === permission.id)) {
          return { permission, value: true };
        }
        return { permission, value: false };
      });
    });
  }

  ngOnInit(): void {
    this.scrollControlService.setScrollable(false);
    this.permissionApiService.getAllPermissions().subscribe((permissions: Permission[]) => {
      this.permissionsAll = permissions;
      this.permissions = permissions.map(permission => ({ permission, value: false }));
    });

    this.roleStore.loadRoles();
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }

  ngOnDestroy(): void {
    this.scrollControlService.setScrollable(true);
  }

  setActiveTab(tabId: string) {
    this.activeTab = tabId;
  }

  selectRole(role: Role) {
    this.roleStore.selectRole(role);
  }

  addNewRole() {
    this.roleApiService.createRole('New Role').subscribe({
      next: (createdRole) => {
        this.roleStore.addRole(createdRole);
      },
      error: () => {
        this.notificationService.show('Failed to create role', 'error');
      }
    });
  }

  updateRolePermissions() {
    if (!this.role) return;

    const currentPermissions = this.role.permissions;
    const addPermissions: number[] = [];
    const removePermissions: number[] = [];

    // Check which permissions need to be added/removed
    this.permissions.forEach(({ permission, value }) => {
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
        this.roleApiService.addPermissionsToRole(
          Number(this.role.id),
          addPermissions
        ).toPromise()
      );
    }

    if (removePermissions.length) {
      updatePromises.push(
        this.roleApiService.removePermissionsFromRole(
          Number(this.role.id),
          removePermissions
        ).toPromise()
      );
    }

    // Execute all updates
    if (updatePromises.length) {
      Promise.all(updatePromises)
        .then(() => {
          this.notificationService.show('Permissions updated successfully', 'success');
          this.role.permissions = this.permissions.filter(p => p.value).map(p => p.permission);
          this.roleStore.updateRole(this.role);
        })
        .catch(() => {
          this.notificationService.show('Failed to update permissions', 'error');
        });
    }
  }

  deleteRole() {
    if (!this.role || !confirm('Are you sure you want to delete this role? This action cannot be undone.')) {
      return;
    }

    this.roleApiService.deleteRole(Number(this.role.id)).subscribe({
      next: () => {
        // Remove role from store
        this.roleStore.removeRole(this.role.id);

        this.notificationService.show('Role deleted successfully', 'success');
      },
      error: () => {
        this.notificationService.show('Failed to delete role', 'error');
      }
    });
  }

  togglePermission(permissionId: number) {
    if (!this.role) return;
    this.permissions.find(p => p.permission.id === permissionId)!.value = !this.permissions.find(p => p.permission.id === permissionId)!.value;
  }

  updateRole() {
    if (!this.role) return;

    const role = this.role!;
    this.roleApiService.updateRole(role.id, {
      name: role.name,
      color: role.color
    }).subscribe({
      next: () => {
        this.notificationService.show('Role updated successfully', 'success');
        this.roleStore.updateRole(role);
      },
      error: () => {
        this.notificationService.show('Failed to update role', 'error');
      }
    });
  }

  searchMembers(query: string) {
    if (!query.trim()) {
      this.filteredMembers = this.members;
      return;
    }

    this.memberService.searchMembers(query).subscribe({
      next: (members) => {
        this.filteredMembers = members;
      },
      error: () => {
        this.notificationService.show('Failed to search members', 'error');
        this.filteredMembers = [];
      }
    });
  }

  isMemberInRole(member: Member): boolean {
    return this.role?.members.some(m => m.id === member.id) ?? false;
  }

  toggleMemberRole(member: Member) {
    if (this.isMemberInRole(member)) {
      // Remove member from role
      this.roleApiService.removeMembersFromRole(Number(this.role.id), [Number(member.id)]).subscribe({
        next: () => {
          this.role!.members = this.role!.members.filter(m => m.id !== member.id);
          this.notificationService.show('Members removed successfully', 'success');
        },
        error: () => {
          this.notificationService.show('Failed to remove members', 'error');
        }
      });
    } else {
      // Add member to role
      this.roleApiService.addMembersToRole(Number(this.role.id), [Number(member.id)]).subscribe({
        next: () => {
          this.role!.members.push(member);
          this.notificationService.show('Members assigned successfully', 'success');
        },
        error: () => {
          this.notificationService.show('Failed to assign members', 'error');
        }
      });
    }
  }
}
