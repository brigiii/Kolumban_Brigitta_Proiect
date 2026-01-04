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

namespace Kolumban_Brigitta_Proiect.Pages.Guests
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
            return Page();
        }

        [BindProperty]
        public Guest Guest { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            bool emailExists = await _context.Guest
            .AnyAsync(g => g.Email == Guest.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Guest.Email",
                    "This email address is already registered.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Guest.Add(Guest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
