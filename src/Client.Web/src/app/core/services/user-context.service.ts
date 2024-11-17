import { inject, Injectable, signal, computed, Signal } from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { OAuthService } from 'angular-oauth2-oidc';
import { UserApiService } from '../../apis/user.api.service';

@Injectable({
  providedIn: 'root'
})
export class UserContextService {

  private authService = inject(OAuthService);
  private userApiService = inject(UserApiService);
  // Create a signal for the user claims
  private userClaims = signal<any>(null);


  userClaims$ = toObservable(this.userClaims);
  constructor() {
    this.userClaims.set(this.authService.getIdentityClaims());
    this.userApiService.createUser().subscribe(user => {
      console.log(user);
    });
    this.authService.events.subscribe(event => {
      if (event.type === 'token_received') {
        this.userClaims.set(this.authService.getIdentityClaims());
        this.userApiService.createUser().subscribe(user => {
          console.log(user);
        });
        console.log(this.userClaims());
      }
    });
  }
}
