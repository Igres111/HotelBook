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
        DbSet<Hotel> Hotels { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Booking> Bookings { get; set; }
    }
}
