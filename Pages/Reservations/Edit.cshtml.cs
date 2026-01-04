using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kolumban_Brigitta_Proiect.Data;
using Kolumban_Brigitta_Proiect.Models;

namespace Kolumban_Brigitta_Proiect.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext _context;

        public EditModel(Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

      
            public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await _context.Reservation
                .Include(r => r.Room)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            ViewData["RoomId"] = new SelectList(
                _context.Room.Include(r => r.Hotel)
                .Where(r => r.Availability),
                "Id",
                "RoomNumber",
                Reservation.RoomId
            );

            ViewData["GuestId"] = new SelectList(
                _context.Guest,
                "ID",
                "Name",
                Reservation.GuestId
            );

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           
            if (Reservation.CheckOutDate <= Reservation.CheckInDate)
            {
                ModelState.AddModelError("",
                    "Check-out date must be after check-in date.");
                return Page();
            }

            
            var room = await _context.Room
                .FirstOrDefaultAsync(r => r.Id == Reservation.RoomId);

            if (room == null)
            {
                ModelState.AddModelError("", "Selected room not found.");
                return Page();
            }

           
            int numberOfNights =
                (Reservation.CheckOutDate.Date - Reservation.CheckInDate.Date).Days;

            if (numberOfNights <= 0)
            {
                ModelState.AddModelError("", "Invalid reservation period.");
                return Page();
            }

            
            Reservation.TotalPrice = numberOfNights * room.PricePerNight;

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ID == id);
        }
    }
}
