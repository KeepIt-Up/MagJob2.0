import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent {
  @Input() alertType: 'info' | 'danger' | 'success' | 'warning' | 'dark' = 'dark';
  @Input({ required: true }) title: string = '';
  @Input() description: string = '';
  @Output() onDismissClick = new EventEmitter();
  @Output() onViewMoreClick = new EventEmitter();
  alertVisible: boolean = true;

  constructor() { }

  dismissAlert(): void {
    this.alertVisible = false;
    this.onDismissClick.emit();
  }
}
