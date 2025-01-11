package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.*;
import lombok.experimental.SuperBuilder;

import java.math.BigInteger;
import java.time.LocalTime;
import java.util.UUID;

@Getter
@Setter
@SuperBuilder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Entity
@Table(name = "time_entry_templates")
public class TimeEntryTemplate {

    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "memberSequenceGenerator")
    @SequenceGenerator(name = "memberSequenceGenerator")
    private BigInteger id;

    @NotNull
    @Column(name = "status", nullable = false)
    private LocalTime startTime;

    @NotNull
    @Column(name = "status", nullable = false)
    private LocalTime endTime;

    @NotNull
    @Column(name = "status", nullable = false)
    private Integer startDayOffset;

    @NotNull
    @Column(name = "status", nullable = false)
    private Integer endDayOffset;

    // Getters and setters
}
