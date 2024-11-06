using Andreea_Zoicas_Lab2.Data;
using Andreea_Zoicas_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Andreea_Zoicas_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryDate> AssignedCategoryDataList { get; set; }

        public void PopulateAssignedCategoryData(Andreea_Zoicas_Lab2Context context, Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            var allCategories = context.Category;
            var bookCategories = book.BookCategories != null
                ? new HashSet<int>(book.BookCategories.Select(c => c.CategoryID))
                : new HashSet<int>();

            AssignedCategoryDataList = new List<AssignedCategoryDate>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryDate
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookCategories(Andreea_Zoicas_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.Category.ID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        BookCategory bookToRemove = bookToUpdate
                            .BookCategories
                            .SingleOrDefault(i => i.CategoryID == cat.ID);

                        context.Remove(bookToRemove);
                    }
                }
            }
        }
    }
}
