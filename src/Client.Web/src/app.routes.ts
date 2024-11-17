import { Routes } from '@angular/router';
import { OrganizationComponent } from './app/features/organization/components/organization/organization.component';
import { MembersTableComponent } from './app/features/organization/components/members/members-table/member-table.component';
import { InvitationTableComponent } from './app/features/organization/components/invitations/invitation-table/invitation-table.component';
import { OrganizationProfilComponent } from './app/features/organization/components/organization/organization-profil/organization-profil.component';
import { OrganizationCreatorComponent } from './app/features/organization/components/organization/organization-creator/organization-creator.component';
import { UserSettingsComponent } from './app/features/user/components/user-settings/user-settings.component';
import { UserComponent } from './app/features/user/components/user/user.component';
import { UserProfileComponent } from './app/features/user/components/user-profile/user-profile.component';
import { UserOrganizationsComponent } from './app/features/user/components/user-organizations/user-organizations.component';
import { UserInvitationsComponent } from './app/features/user/components/user-invitations/user-invitations.component';
import { PublicComponent } from './app/features/public/public/public.component';
import { RolesManagementComponent } from './app/features/organization/components/roles/roles-management/roles-management.component';

export const routes: Routes = [
  { path: '', redirectTo: 'user', pathMatch: 'full' },
  {
    path: 'organization/:organizationId', component: OrganizationComponent, children: [
      { path: '', redirectTo: 'members', pathMatch: 'full' },
      { path: 'members', component: MembersTableComponent },
      { path: 'invitations', component: InvitationTableComponent },
      { path: 'roles', component: RolesManagementComponent },
      { path: 'settings', component: OrganizationProfilComponent },
      { path: '**', redirectTo: 'members' }
    ]
  },
  {
    path: 'user', component: UserComponent, children: [
      { path: '', redirectTo: 'organizations', pathMatch: 'full' },
      { path: 'organizations', component: UserOrganizationsComponent },
      { path: 'invitations', component: UserInvitationsComponent },
      { path: 'settings', component: UserSettingsComponent },
      { path: '**', redirectTo: 'organizations' },
    ]
  },
  {
    path: 'public', component: PublicComponent, children: [
      { path: '', redirectTo: 'create-organization', pathMatch: 'full' },
      { path: 'create-organization', component: OrganizationCreatorComponent },
      { path: '**', redirectTo: 'create-organization' }
    ]
  },
  { path: '**', redirectTo: 'public' }

];
