import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  @Input() isOpen = false;
  @Input({ required: true }) title = '';

  @Output() onCloseClick = new EventEmitter<void>();

  onClose(): void {
    this.isOpen = false;
    this.onCloseClick.emit();
  }
}
