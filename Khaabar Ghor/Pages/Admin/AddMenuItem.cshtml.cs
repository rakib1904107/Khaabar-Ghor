using Khaabar_Ghor.Data;
using Khaabar_Ghor.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Khaabar_Ghor.Pages.Admin
{

    public class AddMenuItemModel : PageModel
    {
        private readonly KhaabarGhorDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddMenuItemModel(KhaabarGhorDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Menu Menu { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            Menu = new Menu();
            ModelState.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            try
            {
                // Handle Image Upload
                if (ImageFile != null)
                {
                    if (!ValidateImage(ImageFile))
                    {
                        ModelState.AddModelError("ImageFile", "Please upload a valid image file (jpg, jpeg, png) under 2MB.");
                        return Page();
                    }

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Menu");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Ensure directory exists
                    Directory.CreateDirectory(uploadsFolder);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    Menu.ImageUrl = "images/menu/" + uniqueFileName;
                }

                // Add to database
                await _context.Menus.AddAsync(Menu);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Menu item added successfully!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while adding menu item. Please try again.");
                return Page();
            }
        }

        private bool ValidateImage(IFormFile file)
        {
            // Check file size (max 2MB)
            if (file.Length > 2 * 1024 * 1024)
                return false;

            // Check file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }
    }
}
