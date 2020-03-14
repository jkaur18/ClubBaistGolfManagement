using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubBaistGolfManagement.Data;
using ClubBaistGolfManagement.Domain;

namespace ClubBaistGolfManagement.Areas.Identity.Pages.Admin.UserRoles
{
    public class DeleteModel : PageModel
    {
        private readonly ClubBaistGolfManagement.Data.ClubBaistManagementContext _context;

        public DeleteModel(ClubBaistGolfManagement.Data.ClubBaistManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AspNetUserRoles AspNetUserRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User).FirstOrDefaultAsync(m => m.UserId == id);

            if (AspNetUserRoles == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);

            if (AspNetUserRoles != null)
            {
                _context.AspNetUserRoles.Remove(AspNetUserRoles);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
