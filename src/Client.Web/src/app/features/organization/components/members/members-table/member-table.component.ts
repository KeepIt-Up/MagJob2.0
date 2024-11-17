import { Component, inject, Input, OnInit } from '@angular/core';
import { ColumnDefinition } from '../../../../../shared/components/table/table.component';
import { MemberService } from '../../../../../apis/member.api.service';
import { TableWithPaginationComponent } from '../../../../../shared/components/table-with-pagination/table-with-pagination.component';
import { SearchInputComponent } from '../../../../../shared/components/search-input/search-input.component';
import { PaginationPayload } from '../../../../../shared/components/pagination/pagination.component';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { MemberActionsComponent } from './member-actions.component';
import { NotificationService } from '@shared/services/notification.service';
import { EditMemberModalComponent } from '../edit-member-modal/edit-member-modal.component';
import { Member } from '../../../../../types/member/member';

@Component({
  selector: 'app-members-table',
  standalone: true,
  imports: [TableWithPaginationComponent, SearchInputComponent, RouterLink, EditMemberModalComponent],
  templateUrl: './member-table.component.html',
  styleUrl: './member-table.component.css'
})
export class MembersTableComponent implements OnInit {
  memberService = inject(MemberService);
  private route = inject(ActivatedRoute);
  @Input() organizationId!: string;
  payload!: PaginationPayload<Member, string>;
  notificationService = inject(NotificationService);
  isEditModalOpen = false;
  selectedMember?: Member;

  ngOnInit(): void {
    this.route.parent?.params.subscribe(params => {
      this.organizationId = params['organizationId'];
      this.payload = {
        payload: this.organizationId,
        pageNumber: 1,
        pageSize: 10,
        sortField: "id",
        ascending: true
      };
    });
  }

  columnsConfig: ColumnDefinition<Member>[] = [
    {
      title: "ID",
      modelProp: "id",
    },
    {
      title: "Name",
      modelProp: 'firstName',
      computeValue: (row) => { return row.firstName + " " + row.lastName },
      isSortable: true
    },
    {
      title: "Is Still Member?",
      modelProp: 'archived',
      isSortable: true,
      computeValue: (row) => { return row.archived ? "" : "✅" }
    },
    {
      title: "Actions",
      modelProp: 'id',
      component: {
        type: MemberActionsComponent,
        inputs: (row) => ({
          memberId: row.id,
          onEditCallback: () => this.openEditModal(row),
          onDeleteCallback: () => {
            this.memberService.archiveMember(row.id).subscribe({
              next: (res) => {
                this.payload = {
                  ...this.payload,
                  pageNumber: 1
                }
                this.notificationService.show(
                  "Member archived successfully",
                  "success"
                );
              },
              error: (error) => {
                this.notificationService.show(
                  "Failed to archive member",
                  "error"
                );
              }
            });
          }
        }),
      }
    }
  ];

  openEditModal(member: Member) {
    console.log(member);
    this.selectedMember = member;
    this.isEditModalOpen = true;
  }

  closeEditModal() {
    this.isEditModalOpen = false;
    this.selectedMember = undefined;
  }

  saveMember(updatedMember: Partial<Member>) {
    if (this.selectedMember) {
      this.memberService.updateMember(this.selectedMember.id, updatedMember).subscribe({
        next: () => {
          this.notificationService.show('Member updated successfully', 'success');
          this.closeEditModal();
          // Refresh the table
          this.payload = {
            ...this.payload,
            pageNumber: 1
          };
        },
        error: () => {
          this.notificationService.show('Failed to update member', 'error');
        }
      });
    }
  }
}
