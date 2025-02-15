package com.keepitup.magjobbackend.organization.entity;

import com.keepitup.magjobbackend.announcement.entity.Announcement;
import com.keepitup.magjobbackend.invitation.entity.Invitation;
import com.keepitup.magjobbackend.material.entity.Material;
import com.keepitup.magjobbackend.member.entity.Member;
import com.keepitup.magjobbackend.role.entity.Role;
import com.keepitup.magjobbackend.task.entity.Task;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.*;
import lombok.experimental.SuperBuilder;

import java.math.BigInteger;
import java.time.ZonedDateTime;
import java.util.List;

@Getter
@Setter
@SuperBuilder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Entity
@Table(name = "organizations")
public class Organization {
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "organizationSequenceGenerator")
    @SequenceGenerator(name = "organizationSequenceGenerator")
    private BigInteger id;

    @NotNull
    @Column(name = "name", nullable = false, unique = true)
    private String name;

    @Column(name = "date_of_creation")
    private ZonedDateTime dateOfCreation;

    @Lob
    @Column(name = "banner", length = 1000)
    private byte[] banner;

    @OneToMany(mappedBy = "organization", cascade = CascadeType.REMOVE)
    private List<Member> members;

    @OneToMany(mappedBy = "organization")
    private List<Invitation> invitations;
  
    @OneToMany(mappedBy = "organization")
    private List<Task> tasks;

    @OneToMany(mappedBy = "organization")
    private List<Announcement> announcements;

    @OneToMany(mappedBy = "organization")
    private List<Role> roles;

    @OneToMany(mappedBy = "organization")
    private List<Material> materials;
}
