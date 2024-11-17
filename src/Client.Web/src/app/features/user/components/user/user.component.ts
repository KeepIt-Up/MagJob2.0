import { Component } from '@angular/core';
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { RouterOutlet } from '@angular/router';
import { UserSidebarComponent } from '../../../../core/components/user-sidebar/user-sidebar.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    NavbarComponent,
    RouterOutlet,
    UserSidebarComponent
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  sidebarExpanded = true;

  sidebarExpandedChange(expanded: boolean) {
    this.sidebarExpanded = expanded;
  }
}
