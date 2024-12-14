import { inject, Injectable, signal, computed, Signal } from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { OAuthEvent, OAuthService } from 'angular-oauth2-oidc';
import { User, UserApiService } from '../../apis/user.api.service';
import { EventType } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserContextService {

  private authService = inject(OAuthService);
  private userApiService = inject(UserApiService);
  private user = signal<User | undefined>(undefined);

  $user = this.user.asReadonly();

  constructor() {
    if (this.authService.hasValidAccessToken()) {
      const sub = this.authService.getIdentityClaims()['sub'];
      this.userExistingCheck(sub);
      return;
    }

    this.authService.events.subscribe((event: OAuthEvent) => {
      switch (event.type) {
        case 'token_received':
          console.log(this.authService.getIdentityClaims());
          const sub = this.authService.getIdentityClaims()['sub'];
          this.userExistingCheck(sub);
      }
    });
  }

  init(userId: string) {

  }

  /**
   * Check that owner of the token exist in API database
   * @param userUid user uid form the token
   */
  private userExistingCheck(userUid: string) {
    this.userApiService.getById(userUid).subscribe({
      next: (value: User) => {
        this.user.set(value);
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
    this.userApiService.create().subscribe({
      next: (value) => {
        this.user.set(value);
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
