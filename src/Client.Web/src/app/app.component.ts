import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NotificationsComponent } from '@shared/components/notifications/notifications.component';
import { UserContextService } from './core/services/user-context.service';
import { SpinnerComponent } from '@shared/components/spinner/spinner.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  standalone: true,
  imports: [RouterOutlet, NotificationsComponent, SpinnerComponent]
})
export class AppComponent {
  private userContext = inject(UserContextService);

  $user = this.userContext.$user;
}
