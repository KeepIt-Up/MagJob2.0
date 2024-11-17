import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchModalComponent } from '@shared/components/search-modal/search-modal.component';
import { Member } from '../../../../../types/member/member';
import { Role } from '../../../../../types/role/role';

@Component({
    selector: 'app-role-members',
    standalone: true,
    imports: [CommonModule, SearchModalComponent],
    template: `
    <div class="space-y-4">
      <div class="flex justify-between items-center">
        <h3 class="text-lg font-semibold text-gray-900 dark:text-white">Members</h3>
        <button (click)="showMemberSearchModal = true"
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5">
          Add Members
        </button>
      </div>

      <div class="space-y-2">
        @for (member of role.members; track member.id) {
          <div class="flex justify-between items-center p-3 bg-gray-50 dark:bg-gray-700 rounded-lg">
            <div class="flex items-center gap-3">
              <div class="flex-shrink-0">
                <img class="w-8 h-8 rounded-full" [src]="'assets/profile.jpg'" [alt]="member.firstName">
              </div>
              <p class="text-sm font-medium text-gray-900 dark:text-white">
                {{member.firstName}} {{member.lastName}}
              </p>
            </div>
            <button (click)="memberToggled.emit(member)"
              class="px-4 py-2 text-sm font-medium rounded-lg bg-blue-600 hover:bg-blue-700 text-white transition-colors">
              Remove
            </button>
          </div>
        } @empty {
          <div class="p-3 text-sm text-gray-600 dark:text-gray-400 text-center bg-gray-50 dark:bg-gray-800 rounded-lg">
            No members assigned to this role
          </div>
        }
      </div>
    </div>

    <app-search-modal 
      [isOpen]="showMemberSearchModal" 
      [title]="'Search Members'" 
      [searchPlaceholder]="'Search members...'"
      [items]="filteredMembers" 
      [selectedItems]="role.members" 
      [displayFn]="memberDisplayFn"
      [compareFn]="memberCompareFn"
      (close)="showMemberSearchModal = false"
      (search)="memberSearched.emit($event)"
      (selectionChange)="memberToggled.emit($event)">
    </app-search-modal>
  `
})
export class RoleMembersComponent {
    @Input({ required: true }) role!: Role;
    @Input({ required: true }) filteredMembers: Member[] = [];
    @Output() memberToggled = new EventEmitter<Member>();
    @Output() memberSearched = new EventEmitter<string>();

    showMemberSearchModal = false;

    memberDisplayFn(member: Member): string {
        return `${member.firstName} ${member.lastName}`;
    }

    memberCompareFn(a: Member, b: Member): boolean {
        return a.id === b.id;
    }
} 