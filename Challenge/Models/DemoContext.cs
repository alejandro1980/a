using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Challenge.Models;

namespace Challenge.Models
{
    public class DemoContext : DbContext
    {
        public DemoContext() : base("ConString")
        {

        }
        public DbSet<Socio> Employees { get; set; }
    }
}