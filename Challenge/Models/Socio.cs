using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Challenge.Models
{
    public class Socio
    {/*
        public int EmployeeId { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        public int Salary { get; set; }
        */
        public string SocioName { get; set; }
        public int SocioAge { get; set; }
        public string SocioTeam { get; set; }
        public string SocioCivilStatus { get; set; }
        public string SocioStudies { get; set; }
        


    }
}