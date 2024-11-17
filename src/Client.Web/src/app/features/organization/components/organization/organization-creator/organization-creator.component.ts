import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OrganizationService } from '../../../../../apis/organization.api.service';
import { NotificationService } from '@shared/services/notification.service';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-organization-creator',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './organization-creator.component.html',
  styleUrls: ['./organization-creator.component.css']
})
export class OrganizationCreatorComponent {
  readonly organizationService = inject(OrganizationService);
  readonly notificationService = inject(NotificationService);
  readonly authService = inject(OAuthService);
  organization = {
    name: '',
    description: '',
    logoUrl: '',
    websiteUrl: '',
    location: ''
  };

  onSubmit() {
    // Implement your form submission logic here
    console.log('Form submitted:', this.organization);
    this.organizationService.createOrganization(this.organization).subscribe({
      next: () => {
        this.notificationService.show('Organization created successfully', 'success');
        this.authService.refreshToken();
      },
      error: (error) => {
        this.notificationService.show('Error creating organization', 'error');
      }
    });
  }
}
