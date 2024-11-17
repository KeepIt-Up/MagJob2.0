import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Role } from '../../../../../types/role/role';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-roles-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './roles-list.component.html'
})
export class RolesListComponent {
    @Input() roles: Role[] = [];
    @Input() selectedRole: Role | undefined;
    @Output() roleSelect = new EventEmitter<Role>();

    onRoleSelect(role: Role): void {
        this.roleSelect.emit(role);
    }
} 