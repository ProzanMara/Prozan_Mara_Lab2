using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;

namespace Prozan_Mara_Lab2.Pages.BookPages
{
    public class BookData
    {
        public IList<Book> Books { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<BookCategory> BookCategories { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly Prozan_Mara_Lab2Context _context;

        public IndexModel(Prozan_Mara_Lab2Context context)
        {
            _context = context;
        }

        public BookData BookD { get; set; } = default!;

        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";

            CurrentFilter = searchString;

            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();


            // Filtrare sigură împotriva valorilor nule (null-safe)
            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s =>
                    (s.Author != null && s.Author.FirstName != null && s.Author.FirstName.Contains(searchString)) ||
                    (s.Author != null && s.Author.LastName != null && s.Author.LastName.Contains(searchString)) ||
                    (s.Title != null && s.Title.Contains(searchString))
                ).ToList();
            }

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category).ToList();
            }

            // Sortare sigură împotriva valorilor nule (null-safe)
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author != null ? s.Author.FullName : "").ToList();
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author != null ? s.Author.FullName : "").ToList();
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title).ToList();
                    break;
            }
        }
    }
}