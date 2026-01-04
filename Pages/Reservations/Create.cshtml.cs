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
                _context.Room.Include(r => r.Hotel)
                .Where(r => r.Availability)
                .Select(r => new
                {
                    r.Id,
                    Display = r.RoomNumber + " (" + r.Hotel.Name + ")"
                }),
                "Id",
                "Display"
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

            bool isRoomBooked = await _context.Reservation.AnyAsync(r =>
                r.RoomId == Reservation.RoomId &&
                r.CheckInDate < Reservation.CheckOutDate &&
                r.CheckOutDate > Reservation.CheckInDate
            );

            if (isRoomBooked)
            {
                ModelState.AddModelError("",
                    "This room is already booked for the selected period.");
                return Page();
            }

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}