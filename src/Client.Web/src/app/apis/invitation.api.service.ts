import { Injectable } from '@angular/core';
import { PaginatedResponse, PaginationPayload } from '../shared/components/pagination/pagination.component';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Invitation } from '../types/invitation/invitation';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {
  private readonly apiUrl = '/api/invitation';

  constructor(private http: HttpClient) { }

  getAll(paginationPayload: PaginationPayload<Invitation, string>): Observable<PaginatedResponse<Invitation>> {
    return this.http.post<PaginatedResponse<Invitation>>(`/api/invitation/paginated`, paginationPayload);
  }

  getUserInvitations(userId: string): Observable<Invitation[]> {
    return this.http.get<Invitation[]>(`/api/users/${userId}/invitations`);
  }

  acceptInvitation(invitationId: string): Observable<void> {
    return this.http.post<void>(`/api/invitation/accept/${invitationId}`, {});
  }

  rejectInvitation(invitationId: string): Observable<void> {
    return this.http.post<void>(`/api/invitation/reject/${invitationId}`, {});
  }
}
