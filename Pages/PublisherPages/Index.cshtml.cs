using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;
using Prozan_Mara_Lab2.Models.ViewModels;

namespace Prozan_Mara_Lab2.Pages.PublisherPages
{
    public class IndexModel : PageModel
    {
        private readonly Prozan_Mara_Lab2Context _context;

        public IndexModel(Prozan_Mara_Lab2Context context)
        {
            _context = context;
        }

        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();

            PublisherData.Publishers = await _context.Publisher
                .Include(i => i.Books)
                    .ThenInclude(c => c.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }
        }
    }
}