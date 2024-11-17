import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Organization } from '../types/organization/organization';
import { OAuthService } from 'angular-oauth2-oidc';

export interface OrganizationCreation {
  "name": string;
  "description": string;
  "logoUrl": string;
  "websiteUrl": string;
  "location": string;
}

interface ServerResponse {
  message: string;
  status: 'success' | 'error';
}

export interface UpdateOrganizationPayload {
  name?: string;
  description?: string;
  profileImage?: string;
  bannerImage?: string;
}

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  private http = inject(HttpClient);
  private authService = inject(OAuthService);

  getOrganization(organizationId: string): Observable<Organization> {
    return this.http.get<Organization>(`/api/organizations/${organizationId}`);
  }

  createOrganization(organization: OrganizationCreation): Observable<Organization> {
    const payload = {
      name: organization.name,
      userId: this.authService.getIdentityClaims()['sub'],
    };
    return this.http.post<Organization>('/api/organizations', payload);
  }

  getOrganizationsByUserId(userId: string): Observable<Organization[]> {
    return this.http.get<any>(`/api/organizations/users/${userId}`).pipe(
      map(response => response.organizations)
    );
  }

  updateOrganization(organizationId: string, payload: UpdateOrganizationPayload): Observable<any> {
    return this.http.put(
      `/api/organizations/${organizationId}`,
      payload,
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    );
  }
}
