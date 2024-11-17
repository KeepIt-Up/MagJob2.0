import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ModalComponent } from '../modal/modal.component';
import { ContainerComponent, ContainerState } from "../container/container.component";

interface ServerResponse {
    message: string;
    status: 'success' | 'error';
}

@Component({
    selector: 'app-response-modal',
    standalone: true,
    imports: [CommonModule, ModalComponent, ContainerComponent],
    template: `
    <app-modal [isOpen]="isOpen" [title]="title" (onCloseClick)="onClose()">
        <app-container [source$]="request$" (onStateChange)="stateChange($event)">
            <div class="text-center text-lg font-medium p-4 text-gray-700">
                {{successMessage}}
            </div>
        </app-container>
        <div class="p-4 md:p-5">
            <div class="flex justify-end">
                <button 
                    class="mg-btn mg-btn-sm mg-btn-primary"
                    (click)="onClose()">
                    Close
                </button>
            </div>
        </div>
    </app-modal>
  `
})
export class ResponseModalComponent {
    @Input() isOpen = false;
    @Input() title = 'Server Response';
    @Input() successMessage = 'Operation completed successfully';
    @Input() errorMessage = 'An unexpected error occurred';
    @Input() request$!: Observable<any>;
    @Output() onStateChange = new EventEmitter<ContainerState<any>>();

    @Output() close = new EventEmitter<void>();

    stateChange(state: ContainerState<any>): void {
        this.onStateChange.emit(state);
    }

    onClose(): void {
        this.close.emit();
    }
} 