import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Member } from '@features/models/member/member';
import { BaseApiService } from '@shared/services/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class MemberApiService extends BaseApiService<Member> {

  override readonly apiUrl = '/api/members';

  archiveMember(memberId: string) {
    return this.http.put(`${this.apiUrl}/${memberId}/archive`, {});
  }

  searchMembers(name: string, organizationId: string): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.apiUrl}/search`, {
      params: { name: name, organizationId: organizationId }
    });
  }
}
