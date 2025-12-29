using Kolumban_Brigitta_Proiect.Data;
using Kolumban_Brigitta_Proiect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolumban_Brigitta_Proiect.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext _context;

        public CreateModel(Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext context)
        {
            _context = context;
        }

      public IActionResult OnGet()
        {
            ViewData["RoomId"] = new SelectList(
                _context.Room.Include(r => r.Hotel),
                "Id",
                "RoomNumber"
            );

            ViewData["GuestId"] = new SelectList(
                _context.Guest,
                "ID",
                "Name"
            );

            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
