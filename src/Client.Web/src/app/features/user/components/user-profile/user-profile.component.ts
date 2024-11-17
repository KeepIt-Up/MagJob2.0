import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserContextService } from '../../../../core/services/user-context.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent {

  userContextService = inject(UserContextService);
}
