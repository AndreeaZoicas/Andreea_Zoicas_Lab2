using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andreea_Zoicas_Lab2.Data;
using Andreea_Zoicas_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Andreea_Zoicas_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly Andreea_Zoicas_Lab2.Data.Andreea_Zoicas_Lab2Context _context;

        public DetailsModel(Andreea_Zoicas_Lab2.Data.Andreea_Zoicas_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                 .Include(b => b.Publisher)
                 .Include(b => b.Author)
                 .Include(b => b.BookCategories) // Include relația BookCategories
                     .ThenInclude(bc => bc.Category) // Include categoriile prin BookCategories
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
