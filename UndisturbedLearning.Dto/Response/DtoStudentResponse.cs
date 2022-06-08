﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Response;

public class DtoStudentResponse
{
    public int Id { get; set; }
    public string Code { get; set; }

    public string Dni { get; set; }
    public string Surname { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public string Telephone { get; set; }
    public bool Undergraduate { get; set; }
}
