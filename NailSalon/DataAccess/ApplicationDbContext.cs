using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using NailSalon.Models;
using NailSalon.DataAccess;




namespace NailSalon.DataAccess

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }

    }
}

