
using Khaabar_Ghor.Data;
using Khaabar_Ghor.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Khaabar_Ghor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly KhaabarGhorDbContext _context;

        public IndexModel(KhaabarGhorDbContext context)
        {
            _context = context;
        }

        public List<Menu> MenuItems { get; set; }
        public List<string> Categories { get; set; }

        public async Task OnGetAsync()
        {
            MenuItems = await _context.Menus
                .Where(m => m.IsAvailable)
                .OrderBy(m => m.Category)
                .ThenBy(m => m.Name)
                .ToListAsync();

            Categories = MenuItems
                .Select(m => m.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }
    }
}
