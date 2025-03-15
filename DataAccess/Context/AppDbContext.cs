using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
       public DbSet<Hotel> Hotels { get; set; }
       public DbSet<Room> Rooms { get; set; }
       public DbSet<Booking> Bookings { get; set; }
    }
}
