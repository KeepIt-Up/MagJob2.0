import { Injectable } from '@angular/core';
import { Organization } from '../../../types/organization/organization';
import { OrganizationService } from '../../../apis/organization.api.service';
import { BaseStore } from '../../../shared/stores/base.store';

@Injectable({
    providedIn: 'root'
})
export class OrganizationStore extends BaseStore<Organization> {
    constructor(private organizationService: OrganizationService) {
        super();
    }

    // Actions
    loadOrganization(organizationId: string): void {
        this.setLoading();

        this.organizationService.getOrganization(organizationId).subscribe({
            next: (organization) => this.setData(organization),
            error: (error) => this.setError(error)
        });
    }
} 