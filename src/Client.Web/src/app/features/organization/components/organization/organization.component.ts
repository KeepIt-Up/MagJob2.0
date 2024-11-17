import { Component, inject, Input, OnInit, signal, effect, DestroyRef, OnChanges, SimpleChanges } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { OrganizationSidebarComponent } from "../../../../core/components/organization-sidebar/organization-sidebar.component";
import { NavbarComponent } from "../../../../core/components/navbar/navbar.component";
import { OrganizationStore } from '../../stores/organization.store';
import { SpinnerComponent } from '../../../../shared/components/spinner/spinner.component';
import { HttpErrorResponseAlertComponent } from '../../../../shared/components/http-error-response-alert/http-error-response-alert.component';
import { HttpErrorResponse } from '@angular/common/http';
import { ScrollControlService } from '../../../../core/services/scroll-control.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-organization',
  standalone: true,
  imports: [RouterOutlet, OrganizationSidebarComponent, NavbarComponent, HttpErrorResponseAlertComponent, SpinnerComponent],
  templateUrl: './organization.component.html',
  styleUrl: './organization.component.css'
})
export class OrganizationComponent implements OnInit {
  @Input() organizationId!: string;
  readonly organizationStore = inject(OrganizationStore);
  private scrollControlService = inject(ScrollControlService);
  readonly organization = this.organizationStore.data;
  readonly loading = this.organizationStore.loading;
  readonly error = this.organizationStore.error;

  sidebarExpanded = false;
  scrollable = this.scrollControlService.scrollable$;

  ngOnInit(): void {
    this.organizationStore.loadOrganization(this.organizationId);
  }

  sidebarExpandedChange(expanded: boolean) {
    this.sidebarExpanded = expanded;
  }

  httpError(): HttpErrorResponse | undefined {
    if (this.organizationStore.error() instanceof HttpErrorResponse) {
      return this.organizationStore.error() as HttpErrorResponse;
    }
    return new HttpErrorResponse({ status: 500 });
  }
}
