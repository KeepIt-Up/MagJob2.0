package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.*;

import java.util.UUID;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor(access = AccessLevel.PRIVATE)
@ToString
@EqualsAndHashCode
@Schema(description = "PostOrganizationRequest DTO")
public class PostTimeEntryTemplateRequest {

    @Schema(description = "Organization name value")
    private String name;

    @Schema(description = "Organization profile banner")
    private byte[] banner;

    @Schema(description = "User id value")
    private UUID userId;

}