import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Organization } from '@features/models/organization/organization';
import { PaginatedResponse, PaginationOptions, serializePaginationOptions } from '@shared/components/pagination/pagination.component';
import { Member } from '@features/models/member/member';
import { Invitation } from '@features/models/invitation/invitation';
import { BaseApiService } from '@shared/services/base-api.service';

export interface CreateOrganizationPayload {
  name: string;
  description?: string;
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
export class OrganizationApiService extends BaseApiService<Organization> {

  override readonly apiUrl = '/api/organizations';

  getInvitations(query: Record<any, any>, paginationOptions: PaginationOptions<Invitation>): Observable<PaginatedResponse<Invitation>> {
    const options = serializePaginationOptions(paginationOptions);
    return this.http.get<PaginatedResponse<Invitation>>(`${this.apiUrl}/invitations`, { params: { ...query, ...options } });
  }
}
