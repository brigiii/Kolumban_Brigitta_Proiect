using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kolumban_Brigitta_Proiect.Models;

namespace Kolumban_Brigitta_Proiect.Data
{
    public class Kolumban_Brigitta_ProiectContext : DbContext
    {
        public Kolumban_Brigitta_ProiectContext (DbContextOptions<Kolumban_Brigitta_ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Kolumban_Brigitta_Proiect.Models.Hotel> Hotel { get; set; } = default!;
        public DbSet<Kolumban_Brigitta_Proiect.Models.Room> Room { get; set; } = default!;
        public DbSet<Kolumban_Brigitta_Proiect.Models.Guest> Guest { get; set; } = default!;
        public DbSet<Kolumban_Brigitta_Proiect.Models.Reservation> Reservation { get; set; } = default!;
        
    }
}
