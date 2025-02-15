package com.keepitup.magjobbackend.role.function;

import com.keepitup.magjobbackend.role.dto.GetRoleResponse;
import com.keepitup.magjobbackend.role.entity.Role;
import org.springframework.stereotype.Component;

import java.util.function.Function;

@Component
public class RoleToResponseFunction implements Function<Role, GetRoleResponse> {
    @Override
    public GetRoleResponse apply(Role role) {
        return GetRoleResponse.builder()
                .id(role.getId())
                .externalId(role.getExternalId())
                .name(role.getName())
                .canManageRoles(role.getCanManageRoles())
                .canManageTasks(role.getCanManageTasks())
                .canManageInvitations(role.getCanManageInvitations())
                .canManageAnnouncements(role.getCanManageAnnouncements())
                .organization(GetRoleResponse.Organization.builder()
                        .id(role.getOrganization().getId())
                        .name(role.getOrganization().getName())
                        .build())
                .build();
    }
}
