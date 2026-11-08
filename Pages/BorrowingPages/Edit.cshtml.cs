using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prozan_Mara_Lab2.Models;
using Prozan_Mara_Lab2;

namespace Prozan_Mara_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Prozan_Mara_Lab2Context _context;

    public EditModel(Prozan_Mara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        Borrowing = borrowing;

        ViewData["BookID"] = new SelectList(_context.Book.Select(b => new { b.ID, TitleString = b.Title }), "ID", "TitleString");
        ViewData["MemberID"] = new SelectList(_context.Member.Select(m => new { m.ID, FullName = m.FirstName + " " + m.LastName }), "ID", "FullName");

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["BookID"] = new SelectList(_context.Book.Select(b => new { b.ID, TitleString = b.Title }), "ID", "TitleString");
            ViewData["MemberID"] = new SelectList(_context.Member.Select(m => new { m.ID, FullName = m.FirstName + " " + m.LastName }), "ID", "FullName");
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
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

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }
}