package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.service.api;

import com.keepitup.calendar.api.Calendar.API.timeentrytemplate.entity.TimeEntryTemplate;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;

import java.math.BigInteger;
import java.time.ZonedDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

public interface TimeEntryTemplateService {
    Optional<Page<TimeEntryTemplate>> findAllTimeEntryTemplatesByUser(UUID userId, PageRequest pageRequest);

    List<TimeEntryTemplate> findAll();

    Page<TimeEntryTemplate> findAll(Pageable pageable);

    Optional<TimeEntryTemplate> find(BigInteger id);

    Optional<TimeEntryTemplate> findByName(String name);

    Page<TimeEntryTemplate> findAllByDateOfCreation(ZonedDateTime dateOfCreation, Pageable pageable);

    void create(TimeEntryTemplate organization);

    void delete(BigInteger id);

    void update(TimeEntryTemplate organization);
}
