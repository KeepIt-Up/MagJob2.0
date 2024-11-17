import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ContainerComponent } from '../../../../shared/components/container/container.component';

@Component({
  selector: 'app-organization-list',
  standalone: true,
  imports: [ContainerComponent],
  templateUrl: './organization-list.component.html',
  styleUrl: './organization-list.component.css'
})
export class OrganizationListComponent {
  source$?: Observable<any>;
  state: any;
}
