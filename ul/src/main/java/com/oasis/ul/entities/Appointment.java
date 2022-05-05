package com.oasis.ul.entities;

import lombok.Data;

import javax.persistence.*;
import java.util.Date;

@Data
@Entity
@Table(name = "appointments")
public class Appointment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn
    private Psychopedagogist psychopedagogist;
    @ManyToOne(fetch = FetchType.LAZY, cascade = CascadeType.PERSIST)
    @JoinColumn
    private Student student;

    @Column(name = "start_time", nullable = false)
    @Temporal(TemporalType.DATE)
    private Date startTime;
    @Column(name = "end_time", nullable = false)
    @Temporal(TemporalType.DATE)
    private Date endTime;
    @Column(name = "cause", length = 140)
    private String cause;
    @Column(name = "comment")
    private String comment;
    @Column(name = "reminder",nullable = false, columnDefinition = "boolean default false")
    private Boolean reminder;
    @Column(name = "rating")
    private Short rating;
}
