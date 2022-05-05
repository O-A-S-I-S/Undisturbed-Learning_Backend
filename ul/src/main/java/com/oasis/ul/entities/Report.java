package com.oasis.ul.entities;

import lombok.Data;

import javax.persistence.*;

@Data
@Entity
@Table(name = "reports")
public class Report {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(name = "resolution", nullable = false, length = 100)
    private String resolution;
    @Column(name = "brief", nullable = false, length = 200)
    private String brief;
    @Column(name = "description", nullable = false)
    private String description;

    @OneToOne
    @JoinColumn(name = "appointment", nullable = false)
    private Appointment appointment;
}
