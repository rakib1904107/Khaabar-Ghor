using Khaabar_Ghor.Data;
using Khaabar_Ghor.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Khaabar_Ghor.Pages.Admin
{

    public class EditItemModel : PageModel
    {
        private readonly KhaabarGhorDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditItemModel(KhaabarGhorDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Menu MenuItem { get; set; }

        [BindProperty]
        public IFormFile NewImage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MenuItem = await _context.Menus.FindAsync(id);

            if (MenuItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                var menuItem = await _context.Menus.FindAsync(MenuItem.Id);

                if (menuItem == null)
                {
                    return NotFound();
                }

                // Update the image if a new one is uploaded
                if (NewImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Menu");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + NewImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(uploadsFolder);

                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(menuItem.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                            menuItem.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save new image
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(fileStream);
                    }

                    menuItem.ImageUrl = "images/menu/" + uniqueFileName;
                }

                // Update other properties
                menuItem.Name = MenuItem.Name;
                menuItem.Price = MenuItem.Price;
                menuItem.Category = MenuItem.Category;
                menuItem.IsAvailable = MenuItem.IsAvailable;

                _context.Attach(menuItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Menu item updated successfully!";
                return RedirectToPage("../Menu");
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while updating the menu item. Please try again.";
                return Page();
            }
        }
    }
}
