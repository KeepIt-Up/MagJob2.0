package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.*;

import java.math.BigInteger;
import java.time.ZonedDateTime;
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Schema(description = "GetTimeEntryResponse DTO")
public class GetTimeEntryTemplateResponse {

    @Schema(description = "Organization id value")
    private BigInteger id;

    @Schema(description = "Organization date of creation value")
    private ZonedDateTime dateOfCreation;

    @Schema(description = "Organization name value")
    private String name;

    @Schema(description = "Organization profile banner")
    private byte[] banner;
}