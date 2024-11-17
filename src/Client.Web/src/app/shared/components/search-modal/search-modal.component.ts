import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from '../modal/modal.component';
import { SearchInputComponent } from '../search-input/search-input.component';

@Component({
    selector: 'app-search-modal',
    standalone: true,
    imports: [CommonModule, FormsModule, ModalComponent, SearchInputComponent],
    template: `
    <app-modal [isOpen]="isOpen" [title]="title" (onCloseClick)="close.emit()">
      <div class="p-4 md:p-5">
        <!-- Search input -->
        <div class="mb-4">
          <app-search-input
            [placeholder]="searchPlaceholder"
            (onValueChange)="onSearch($event)"
          ></app-search-input>
        </div>

        <!-- Results list -->
        <div class="space-y-2 max-h-60 overflow-y-auto">
          @for (item of items; track trackBy(item)) {
            <div class="flex items-center justify-between p-3 bg-gray-50 dark:bg-gray-700 rounded-lg">
              <div class="flex items-center">
                <div class="ml-3">
                  <p class="text-sm font-medium text-gray-900 dark:text-white">
                    {{ displayFn(item) }}
                  </p>
                </div>
              </div>
              <button 
                (click)="selectItem(item)"
                class="px-4 py-2 text-sm font-medium rounded-lg"
                [class.bg-blue-600]="isSelected(item)"
                [class.hover:bg-blue-700]="isSelected(item)"
                [class.bg-gray-200]="!isSelected(item)"
                [class.hover:bg-gray-300]="!isSelected(item)"
                [class.text-white]="isSelected(item)"
              >
                {{ isSelected(item) ? 'Remove' : 'Add' }}
              </button>
            </div>
          }
          @if (items.length === 0) {
            <div class="p-3 text-sm text-gray-600 dark:text-gray-400 text-center bg-gray-50 dark:bg-gray-800 rounded-lg">
              No results found
            </div>
          }
        </div>
      </div>
    </app-modal>
  `
})
export class SearchModalComponent<T> {
    @Input() isOpen = false;
    @Input() title = 'Search';
    @Input() searchPlaceholder = 'Search...';
    @Input() items: T[] = [];
    @Input() selectedItems: T[] = [];
    @Input() displayFn: (item: T) => string = (item) => String(item);
    @Input() trackBy: (item: T) => any = (item) => item;
    @Input() compareFn: (a: T, b: T) => boolean = (a, b) => a === b;

    @Output() close = new EventEmitter<void>();
    @Output() search = new EventEmitter<string>();
    @Output() selectionChange = new EventEmitter<T>();

    onSearch(query: string): void {
        this.search.emit(query);
    }

    selectItem(item: T): void {
        this.selectionChange.emit(item);
    }

    isSelected(item: T): boolean {
        return this.selectedItems.some(selected => this.compareFn(selected, item));
    }
} 