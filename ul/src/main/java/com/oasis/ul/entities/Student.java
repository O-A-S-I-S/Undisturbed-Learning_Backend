package com.oasis.ul.entities;

import com.oasis.ul.util.Campus;
import com.oasis.ul.util.Career;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.Date;
import java.util.Set;

@Data
@Entity
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "students")
public class Student {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(name = "code", nullable = false, unique = true, length = 9)
    private String code;
    @Column(name = "password", nullable = false, length = 30)
    private String password;
    @Column(name = "dni", nullable = false, length = 9)
    private String dni;
    @Column(name = "surname", nullable = false, length = 30)
    private String surname;
    @Column(name = "last_name", nullable = false, length = 30)
    private String lastName;
    @Column(name = "birth_date", nullable = false)
    @Temporal(value = TemporalType.DATE)
    private Date birthDate;
    @Column(name = "email", nullable = false, length = 25)
    private String email;
    @Column(name = "cellphone", nullable = true, length = 9)
    private String cellphone;
    @Column(name = "undergraduate", nullable = false)
    private Boolean undergraduate;
    @Column(name = "career", nullable = false)
    private Career career;
    @Column(name = "campus", nullable = false)
    private Campus campus;

    @ManyToMany(mappedBy = "students")
    private Set<Workshop> workshops;
}
