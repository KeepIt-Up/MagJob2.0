import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { UserContextService } from '../../services/user-context.service';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-user-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './user-sidebar.component.html',
  styleUrl: './user-sidebar.component.css'
})
export class UserSidebarComponent {
  private authService = inject(OAuthService);
  userContextService = inject(UserContextService);
  sidebarExpanded: boolean = true;
  @Output() sidebarExpandedChange = new EventEmitter<boolean>();

  toggle() {
    this.sidebarExpanded = !this.sidebarExpanded;
    this.sidebarExpandedChange.emit(this.sidebarExpanded);
  }

  $user = this.userContextService.$user;

  logOut() {
    this.authService.logOut();
  }
}
