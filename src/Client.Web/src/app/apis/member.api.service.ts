import { computed, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResponse, PaginationPayload } from '../shared/components/pagination/pagination.component';
import { Member } from '../types/member/member';
import { OrganizationStore } from '../features/organization/stores/organization.store';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  private readonly apiUrl = '/api/member/paginated';

  constructor(private http: HttpClient, private organizationStore: OrganizationStore) { }
  organizationId = computed(() => this.organizationStore.data()!.id);

  getAll(paginationPayload: PaginationPayload<Member, string>): Observable<PaginatedResponse<Member>> {
    return this.http.post<PaginatedResponse<Member>>('/api/member/paginated', paginationPayload);
  }

  get(userId: string, organizationId: string) {
    return this.http.get<PaginatedResponse<Member>>(`${this.apiUrl}`, {
      params: {
        userId: userId,
        organizationId: organizationId
      }
    });
  }

  archiveMember(memberId: string) {
    return this.http.put(`/api/member/${memberId}/archive`, {});
  }

  updateMember(memberId: string, member: Partial<Member>) {
    return this.http.put(`/api/member/${memberId}`, member);
  }

  searchMembers(name: string): Observable<Member[]> {
    return this.http.get<Member[]>('/api/member/search', {
      params: { name: name, organizationId: this.organizationId() }
    });
  }
}
