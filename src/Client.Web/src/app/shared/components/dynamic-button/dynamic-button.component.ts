import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-dynamic-button',
    template: `
    <button (click)="onClickCallback()">{{ label }}</button>
  `
})
export class DynamicButtonComponent {
    @Input() label!: string;
    @Input() onClickCallback: () => void = () => { };
} 