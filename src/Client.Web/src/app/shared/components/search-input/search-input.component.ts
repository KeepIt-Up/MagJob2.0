import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-search-input',
  standalone: true,
  imports: [],
  templateUrl: './search-input.component.html',
  styleUrl: './search-input.component.css'
})
export class SearchInputComponent {
  @Input() placeholder: string = '';
  @Output() onValueChange = new EventEmitter<string>();
  @Input() disabled: boolean = false;

  clear() {
    this.onValueChange.emit('');
  }
}
