package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.*;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Schema(description = "PatchOrganizationRequest DTO")
public class PatchTimeEntryTemplateRequest {

    @Schema(description = "Organization name value")
    private String name;

    @Schema(description = "Organization profile banner")
    private byte[] banner;
}