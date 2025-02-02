package com.keepitup.calendar.api.Calendar.API.timeentry.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.*;

import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.UUID;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Schema(description = "PatchOrganizationRequest DTO")
public class PatchTimeEntryRequest {
    @Schema(description = "PostTimeEntryRequest Id value")
    private UUID id;

    @Schema(description = "PostTimeEntryRequest startDateTime value")
    private LocalDateTime startDateTime;

    @Schema(description = "PostTimeEntryRequest endDateTime value")
    private LocalDateTime endDateTime;
}