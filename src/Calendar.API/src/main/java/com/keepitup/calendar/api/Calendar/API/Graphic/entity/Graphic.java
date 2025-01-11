package com.keepitup.calendar.api.Calendar.API.Graphic.entity;

import com.keepitup.calendar.api.Calendar.API.timeentrymember.entity.TimeEntryMember;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.RequiredArgsConstructor;
import lombok.NonNull;
import java.math.BigInteger;
import java.util.List;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@RequiredArgsConstructor
public class Graphic {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private BigInteger id;

    @NonNull
    @Column(name = "name", nullable = false)
    private String name;

    @NonNull
    @Column(name = "managerId", nullable = false)
    private BigInteger managerId;

    @OneToMany
    private List<TimeEntryMember> timeEntryMembers;
}
