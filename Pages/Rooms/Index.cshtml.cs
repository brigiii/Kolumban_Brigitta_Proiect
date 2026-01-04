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

namespace Kolumban_Brigitta_Proiect.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext _context;

        public IndexModel(Kolumban_Brigitta_Proiect.Data.Kolumban_Brigitta_ProiectContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int? HotelId { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList Hotels { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ToDate { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Room> roomsQuery = _context.Room
                .Include(r => r.Hotel)
                .Include(r => r.Reservations);

            Hotels = new SelectList(
                _context.Hotel,
                "ID",
                "Name"
            );

          
            if (HotelId != null)
            {
                roomsQuery = roomsQuery.Where(r => r.HotelId == HotelId);
            }

            
            if (FromDate != null && ToDate != null)
            {
                roomsQuery = roomsQuery.Where(room =>
                    !room.Reservations.Any(res =>
                        res.CheckInDate < ToDate &&
                        res.CheckOutDate > FromDate
                    )
                );
            }

            Room = await roomsQuery
                .OrderBy(r => r.RoomNumber)
                .ToListAsync();
        }

    }
}
