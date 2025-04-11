
using Khaabar_Ghor.Data;
using Khaabar_Ghor.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Khaabar_Ghor.Pages
{
    public class MenuModel : PageModel
    {
        private readonly KhaabarGhorDbContext _context;

        public MenuModel(KhaabarGhorDbContext context)
        {
            _context = context;
        }

        public List<Menu> MenuItems { get; set; }

        public async Task OnGetAsync()
        {
            MenuItems = await _context.Menus
                .Where(m => m.IsAvailable)
                .OrderBy(m => m.Category)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var menuItem = await _context.Menus.FindAsync(id);

            if (menuItem != null)
            {
                _context.Menus.Remove(menuItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

    }
}
