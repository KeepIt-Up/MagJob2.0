import { inject, Injectable } from '@angular/core';
import { catchError, EMPTY, tap } from 'rxjs';
import { NotificationService } from '@shared/services/notification.service';
import { MemberApiService } from '@features/apis/member.api.service';
import { Member } from '@features/models/member/member';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  private memberApiService = inject(MemberApiService);
  private notificationService = inject(NotificationService);

  archiveMember(memberId: string) {
    return this.memberApiService.archiveMember(memberId).pipe(
      tap(() => {
        this.notificationService.show('Member archived successfully', 'success');
      }),
      catchError(() => {
        this.notificationService.show('Failed to archive member', 'error');
        return EMPTY;
      })
    );
  }

  updateMember(memberId: string, member: Partial<Member>) {
    return this.memberApiService.update(memberId, member).pipe(
      tap(() => {
        this.notificationService.show('Member updated successfully', 'success');
      }),
      catchError(() => {
        this.notificationService.show('Failed to update member', 'error');
        return EMPTY;
      })
    );
  }
}
