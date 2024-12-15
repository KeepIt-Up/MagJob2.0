import { inject, Injectable, signal } from '@angular/core';
import { UserApiService } from '../apis/user.api';
import { StateService } from '@shared/services/state.service';
import { OAuthEvent, OAuthService } from 'angular-oauth2-oidc';
import { catchError, map, Observable, tap } from 'rxjs';
import { PaginatedResponse, PaginationOptions } from '@shared/components/pagination/pagination.component';
import { Organization } from '@features/models/organization/organization';
import { Invitation } from '@features/models/invitation/invitation';
import { ListStateService } from '@shared/services/list-state.service';
import { User } from '@features/models/user/user';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userState = new StateService<User>();
  private invitationStateService = new ListStateService<Invitation, { endOfData: boolean }>();
  private organizationStateService = new ListStateService<Organization, { endOfData: boolean }>();

  private authService = inject(OAuthService);
  private api = inject(UserApiService);

  //private user = signal<User | undefined>(undefined);

  //$user = this.user.asReadonly();

  constructor() {
    if (this.authService.hasValidAccessToken()) {
      const sub = this.authService.getIdentityClaims()['sub'];
      this.userExistingCheck(sub);
      return;
    }

    this.authService.events.subscribe((event: OAuthEvent) => {
      switch (event.type) {
        case 'token_received':
          const sub = this.authService.getIdentityClaims()['sub'];
          this.userExistingCheck(sub);
      }
    });
  }

  userState$ = this.userState.state$;
  invitationState$ = this.invitationStateService.state$;
  organizationState$ = this.organizationStateService.state$;

  organizationsPaginationOptions$ = signal<PaginationOptions<Organization>>({
    pageNumber: 1,
    pageSize: 10,
    sortField: "id",
    ascending: true
  });

  invitationsPaginationOptions$ = signal<PaginationOptions<Invitation>>({
    pageNumber: 1,
    pageSize: 10,
    sortField: "id",
    ascending: true
  });

  getUserOrganizations(query: Record<any, any>): Observable<PaginatedResponse<Organization>> {
    return this.api.getUserOrganizations(query, this.organizationsPaginationOptions$()).pipe(
      tap((res: any) => {
        //TODO: refactor
        const items = res.organizations;
        res.items = items;
        delete res.organizations;
        const response = res as PaginatedResponse<Organization>;
        //END OF REFACTOR
        if (response.items.length > 0) {
          this.organizationStateService.add(response.items);
          this.organizationStateService.setMetadata({ endOfData: !response.hasNextPage });
        } else {
          this.organizationStateService.setError(new Error('You have no organizations'));
        }
      }),
      catchError((error) => {
        this.organizationStateService.setError(error);
        throw error;
      })
    );
  }

  getUserInvitations(query: Record<any, any>): Observable<PaginatedResponse<Invitation>> {
    return this.api.getUserInvitations(query, this.invitationsPaginationOptions$()).pipe(
      tap((res: any) => {
        //TODO: refactor
        const items = res.invitations;
        res.items = items;
        delete res.organizations;
        const response = res as PaginatedResponse<Invitation>;
        //END OF REFACTOR
        if (response.items.length > 0) {
          this.invitationStateService.add(response.items);
          this.invitationStateService.setMetadata({ endOfData: !response.hasNextPage });
        } else {
          this.invitationStateService.setError(new Error('You have no invitations'));
        }
      }),
      catchError((error) => {
        this.invitationStateService.setError(error);
        throw error;
      })
    );
  }


  /**
   * Check that owner of the token exist in API database
   * @param userUid user uid form the token
   */
  private userExistingCheck(userUid: string) {
    this.api.getUser(userUid).subscribe({
      next: (value: User) => {
        this.userState.setData(value);
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 404) {
          this.createUser();
        }
      },
    });
  }

  /**
   * Create user from the token
   */
  private createUser() {
    this.api.create().subscribe({
      next: (value) => {
        this.userState.setData(value);
      },
      error: (error) => {
        window.confirm(
          'We have a problem with your account. Please contact with our support or try again'
        );
        this.authService.logOut();
      },
    });
  }

}
