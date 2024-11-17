import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-dropdown',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './dropdown.component.html'
})
export class DropdownComponent {
    @Input() isOpen = false;
    @Input() userName = '';
    @Input() userEmail = '';
} 