import { CommonModule } from '@angular/common';
import { Component, effect, inject, OnInit } from '@angular/core';
import { ContainerComponent, ContainerState } from '../../../../shared/components/container/container.component';
import { InvitationService } from '../../../../apis/invitation.api.service';
import { Invitation, InvitationStatus } from '../../../../types/invitation/invitation';
import { UserContextService } from '../../../../core/services/user-context.service';
import { TagComponent } from '../../../../shared/components/tag/tag.component';
import { Observable } from 'rxjs';
import { NotificationService } from '@shared/services/notification.service';

@Component({
  selector: 'app-user-invitations',
  standalone: true,
  imports: [CommonModule, TagComponent, ContainerComponent],
  templateUrl: './user-invitations.component.html',
  styleUrl: './user-invitations.component.css'
})
export class UserInvitationsComponent implements OnInit {
  invitationService = inject(InvitationService);
  userContextService = inject(UserContextService);
  notificationService = inject(NotificationService);
  InvitationStatus = InvitationStatus;
  invitations$!: Observable<Invitation[]>;
  state: ContainerState<Invitation[]> = {
    isLoading: true
  };

  ngOnInit(): void {
    const userId = this.userContextService.$user()!.id;
    this.invitations$ = this.invitationService.getUserInvitations(userId);
  }

  acceptInvitation(invitationId: string): void {
    this.invitationService.acceptInvitation(invitationId).subscribe({
      next: () => {
        this.ngOnInit();
        this.notificationService.show('Invitation accepted successfully', 'success');
      },
      error: () => {
        this.notificationService.show('Error accepting invitation', 'error');
      }
    })
  }

  rejectInvitation(invitationId: string): void {
    this.invitationService.rejectInvitation(invitationId).subscribe({
      next: () => {
        this.ngOnInit();
        this.notificationService.show('Invitation rejected successfully', 'success');
      },
      error: () => {
        this.notificationService.show('Error rejecting invitation', 'error');
      }
    });
  }

  getStatusVariant(status: number): 'yellow' | 'green' | 'red' | 'gray' {
    switch (status) {
      case 0: return 'yellow';
      case 1: return 'green';
      case 2: return 'red';
      case 3: return 'gray';
      default: return 'gray';
    }
  }
}
