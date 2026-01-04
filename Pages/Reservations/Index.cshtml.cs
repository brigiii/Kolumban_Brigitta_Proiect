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

        [BindProperty(SupportsGet = true)]
        public string? SearchGuest { get; set; }

        public IList<Reservation> Reservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<Reservation> reservationsQuery = _context.Reservation
                .Include(r => r.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(r => r.Guest);

            if (!string.IsNullOrEmpty(SearchGuest))
            {
                reservationsQuery = reservationsQuery.Where(r =>
                    r.Guest.Name.Contains(SearchGuest));
            }

            Reservation = await reservationsQuery
                .OrderBy(r => r.CheckInDate)
                .ToListAsync();
        }

    }
}
