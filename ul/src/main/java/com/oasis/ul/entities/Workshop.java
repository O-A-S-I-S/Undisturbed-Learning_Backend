package com.oasis.ul.entities;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.Date;
import java.util.List;
import java.util.Set;

@Data
@Entity
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "students")
public class Workshop {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(nullable = false)
    private Psychopedagogist psychopedagogist;

    @Column(name = "start_time", nullable = false)
    @Temporal(TemporalType.DATE)
    private Date startTime;
    @Column(name = "end_time", nullable = false)
    @Temporal(TemporalType.DATE)
    private Date endTime;
    @Column(name = "title", nullable = false, length = 50)
    private String title;
    @Column(name = "brief", nullable = false, length = 200)
    private String brief;
    @Column(name = "description", nullable = false, columnDefinition = "TEXT")
    private String description;
    @Column(name = "comment")
    private String comment;
    @Column(name = "reminder",nullable = false, columnDefinition = "boolean default false")
    private Boolean reminder;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(name = "workshop_student", joinColumns = @JoinColumn(name = "workshop_id"), inverseJoinColumns = @JoinColumn(name = "student_id"))
    private Set<Student> students;
}
