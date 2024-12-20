import { Member } from "../member/member";

export interface Role {
    id: string,
    name: string,
    description?: string,
    color?: string,
    organizationId: string,
    permissions: Permission[],
    members: Member[]
}

export interface Permission {
    id: string,
    name: string
}

export interface CreateRolePayload {
    name: string;
    organizationId: string;
}

export interface AssignMembersPayload {
    roleId: string,
    roleMembers: { memberId: string }[]
}