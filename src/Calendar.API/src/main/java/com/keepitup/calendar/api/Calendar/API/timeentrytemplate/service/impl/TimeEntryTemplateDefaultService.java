package com.keepitup.calendar.api.Calendar.API.timeentrytemplate.service.impl;

import com.keepitup.calendar.api.Calendar.API.timeentrytemplate.entity.TimeEntryTemplate;
import com.keepitup.calendar.api.Calendar.API.timeentrytemplate.repository.api.TimeEntryTemplateRepository;
import com.keepitup.calendar.api.Calendar.API.timeentrytemplate.service.api.TimeEntryTemplateService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

import java.math.BigInteger;
import java.time.ZonedDateTime;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Service
public class TimeEntryTemplateDefaultService implements TimeEntryTemplateService {
    private final TimeEntryTemplateRepository timeEntryTemplateRepository;

    @Autowired
    public TimeEntryTemplateDefaultService(TimeEntryTemplateRepository timeEntryTemplateRepository) {
        this.timeEntryTemplateRepository = timeEntryTemplateRepository;
    }

    @Override
    public Optional<Page<TimeEntryTemplate>> findAllTimeEntryTemplatesByUser(UUID userId, PageRequest pageRequest) {
        return Optional.empty();
    }

    @Override
    public List<TimeEntryTemplate> findAll() {
        return timeEntryTemplateRepository.findAll();
    }

    @Override
    public Page<TimeEntryTemplate> findAll(Pageable pageable) {
        return timeEntryTemplateRepository.findAll(pageable);
    }

    @Override
    public Optional<TimeEntryTemplate> find(BigInteger id) {
        return timeEntryTemplateRepository.findById(id);
    }

    @Override
    public Optional<TimeEntryTemplate> findByName(String name) {
        return timeEntryTemplateRepository.findByName(name);
    }

    @Override
    public Page<TimeEntryTemplate> findAllByDateOfCreation(ZonedDateTime dateOfCreation, Pageable pageable) {
        return timeEntryTemplateRepository.findAllByDateOfCreation(dateOfCreation, pageable);
    }

    @Override
    public void create(TimeEntryTemplate timeEntryTemplate) {
        timeEntryTemplateRepository.save(timeEntryTemplate);
    }

    @Override
    public void delete(BigInteger id) {
        timeEntryTemplateRepository.findById(id).ifPresent(timeEntryTemplateRepository::delete);
    }

    @Override
    public void update(TimeEntryTemplate timeEntryTemplate) {
        timeEntryTemplateRepository.save(timeEntryTemplate);
    }
}
