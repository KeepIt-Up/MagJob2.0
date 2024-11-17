import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchModalComponent } from '@shared/components/search-modal/search-modal.component';
import { Member } from '../../../../../types/member/member';

@Component({
    selector: 'app-member-search-modal',
    standalone: true,
    imports: [CommonModule, SearchModalComponent],
    template: `
    <app-search-modal 
      [isOpen]="isOpen" 
      [title]="'Search Members'" 
      [searchPlaceholder]="'Search members...'"
      [items]="filteredMembers" 
      [selectedItems]="selectedMembers" 
      [displayFn]="memberDisplayFn"
      [compareFn]="memberCompareFn"
      (close)="closeModal()"
      (search)="searchMembers($event)"
      (selectionChange)="memberToggled.emit($event)">
    </app-search-modal>
  `
})
export class MemberSearchModalComponent {
    @Input() isOpen = false;
    @Input() filteredMembers: Member[] = [];
    @Input() selectedMembers: Member[] = [];

    @Output() close = new EventEmitter<void>();
    @Output() search = new EventEmitter<string>();
    @Output() memberToggled = new EventEmitter<Member>();

    memberDisplayFn(member: Member): string {
        return `${member.firstName} ${member.lastName}`;
    }

    memberCompareFn(a: Member, b: Member): boolean {
        return a.id === b.id;
    }

    closeModal(): void {
        this.close.emit();
    }

    searchMembers(query: string): void {
        this.search.emit(query);
    }
} 