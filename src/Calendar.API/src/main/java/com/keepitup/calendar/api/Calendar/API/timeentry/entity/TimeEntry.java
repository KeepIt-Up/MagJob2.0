package com.keepitup.calendar.api.Calendar.API.timeentry.entity;
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
@Table(name = "time_entrys")
@Entity
public class TimeEntry {

    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "memberSequenceGenerator")
    @SequenceGenerator(name = "memberSequenceGenerator")
    private BigInteger id;

    @NotNull
    @Column(name = "start_time", nullable = false)
    private LocalTime startTime;

    @NotNull
    @Column(name = "end_time", nullable = false)
    private LocalTime endTime;

    @NotNull
    @Column(name = "start_day_offset", nullable = false)
    private Integer startDayOffset;

    @NotNull
    @Column(name = "end_day_offset", nullable = false)
    private Integer endDayOffset;

    // Getters and setters
}
