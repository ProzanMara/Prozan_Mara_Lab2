using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;

namespace Prozan_Mara_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Prozan_Mara_Lab2Context _context;

    public IndexModel(Prozan_Mara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Publisher = await _context.Set<Publisher>().ToListAsync();
    }
}