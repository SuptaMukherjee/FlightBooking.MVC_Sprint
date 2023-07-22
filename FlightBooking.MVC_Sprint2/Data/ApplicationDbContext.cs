using FlightBooking.MVC_Sprint2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.MVC_Sprint2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
           public DbSet<Payment> Payments { get; set; }
          public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetail> TicketDetails { get; set; }

        public DbSet<CustomUser> customUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TicketBooking>(i => i.HasNoKey());
            base.OnModelCreating(builder);
        }

    }
}
