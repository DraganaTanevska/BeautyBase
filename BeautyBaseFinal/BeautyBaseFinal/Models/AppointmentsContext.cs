using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeautyBaseFinal.Models
{
    public class AppointmentsContext : DbContext
    {
        public DbSet<Available> Available { get; set; }
        public DbSet<Unavailable> Unavailable { get; set; }
        public AppointmentsContext() : base("DefaultConnection") { }
        public static AppointmentsContext Create()
        {
            return new AppointmentsContext();
        }
    }
}