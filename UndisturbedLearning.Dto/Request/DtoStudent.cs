﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL_Testing.Dto.Request
{
    public class DtoStudent
    {
        public string Code { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Dni { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Telephone { get; set; }
        public bool Undergraduate { get; set; }
        public string Career { get; set; }
        public string Campus { get; set; }
    }
}
