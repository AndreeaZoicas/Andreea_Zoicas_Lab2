using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andreea_Zoicas_Lab2.Data;
using Andreea_Zoicas_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Andreea_Zoicas_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Andreea_Zoicas_Lab2.Data.Andreea_Zoicas_Lab2Context _context;

        public IndexModel(Andreea_Zoicas_Lab2.Data.Andreea_Zoicas_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync(int? authorId)
        {
            var authors = await _context.Author.ToListAsync();
            ViewData["AuthorID"] = new SelectList(authors, "ID", "LastName");

           
            IQueryable<Book> booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            
            if (authorId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.AuthorID == authorId);
            }

           
            Book = await booksQuery.ToListAsync();
        }




    }
}
