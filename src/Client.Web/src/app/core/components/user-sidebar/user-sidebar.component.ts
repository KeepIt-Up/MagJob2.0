import { Component, computed, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { UserService } from '@features/services/user.service';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-user-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './user-sidebar.component.html'
})
export class UserSidebarComponent {
  private authService = inject(OAuthService);
  userContext = inject(UserService);
  sidebarExpanded: boolean = true;
  @Output() sidebarExpandedChange = new EventEmitter<boolean>();

  toggle() {
    this.sidebarExpanded = !this.sidebarExpanded;
    this.sidebarExpandedChange.emit(this.sidebarExpanded);
  }

  $user = computed(() => this.userContext.userState$().data);

  logOut() {
    this.authService.logOut();
  }
}
