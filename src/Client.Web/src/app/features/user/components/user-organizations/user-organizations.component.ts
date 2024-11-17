import { Component, inject, OnInit } from '@angular/core';
import { OrganizationService } from '../../../../apis/organization.api.service';
import { UserContextService } from '../../../../core/services/user-context.service';
import { ContainerComponent, ContainerState } from "../../../../shared/components/container/container.component";
import { Organization } from '../../../../types/organization/organization';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { ImageService } from '@shared/services/image.service';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-user-organizations',
  standalone: true,
  imports: [ContainerComponent, CommonModule, RouterLink],
  templateUrl: './user-organizations.component.html',
  styleUrl: './user-organizations.component.css'
})
export class UserOrganizationsComponent implements OnInit {

  private organizationsService = inject(OrganizationService);
  private userContextService = inject(UserContextService);
  private imageService = inject(ImageService);

  organizations$!: Observable<Organization[]>;
  state: ContainerState<Organization[]> = {
    isLoading: true
  };

  ngOnInit() {
    this.userContextService.userClaims$.subscribe(claims => {
      if (claims) {
        this.organizations$ = this.organizationsService.getOrganizationsByUserId(claims['sub']);
      }
    });
  }

  getSafeImageUrl(base64String: string | undefined): SafeUrl | undefined {
    return this.imageService.getSafeImageUrl(base64String);
  }
}
