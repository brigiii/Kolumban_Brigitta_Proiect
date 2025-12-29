using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kolumban_Brigitta_Proiect.Data;
using Kolumban_Brigitta_Proiect.Models;

namespace Kolumban_Brigitta_Proiect.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext _context;

        public IndexModel(Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservation
                .Include(r => r.Guest)
                .Include(r => r.Room).ToListAsync();
        }
    }
}
